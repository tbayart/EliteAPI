﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EliteAPI.Abstractions;
using EliteAPI.Abstractions.Configuration;
using EliteAPI.Abstractions.Events;
using EliteAPI.Abstractions.Readers;
using EliteAPI.Abstractions.Status;
using EliteAPI.Events;
using EliteAPI.Events.Status.Ship;
using EliteAPI.Events.Status.Ship.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EliteAPI;

/// <inheritdoc />
public class EliteDangerousApi : IEliteDangerousApi
{
    /// <inheritdoc />
    public IEliteDangerousApiConfiguration Config { get; }

    /// <inheritdoc />
    public IEventParser Parser { get; }

    /// <inheritdoc />
    public bool IsRunning { get; private set; }

    private readonly ILogger<EliteDangerousApi>? _log;

    private DirectoryInfo _journalsDirectory;
    private DirectoryInfo _optionsDirectory;
    private readonly IReader _reader;
    private Task _mainTask;
    private bool _hasInitialised;

    /// <summary>
    /// Creates a new instance of the EliteDangerousApi class using the services defined in the provided IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to use to create the instance.</param>
    public EliteDangerousApi(IServiceProvider services)
    {
        _log = services.GetService<ILogger<EliteDangerousApi>>();

        Parser = services.GetRequiredService<IEventParser>();
        Config = services.GetRequiredService<IEliteDangerousApiConfiguration>();
        Events = services.GetRequiredService<IEvents>();
        _reader = services.GetRequiredService<IReader>();
    }

    /// <summary>
    /// Creates a new instance of the EliteDangerousApi class.
    /// </summary>
    public static IEliteDangerousApi Create()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureLogging(log => log.ClearProviders())
            .ConfigureServices(services => services.AddEliteApi())
            .Build();

        return host.Services.GetRequiredService<IEliteDangerousApi>();
    }

    /// <inheritdoc />
    public async Task InitialiseAsync()
    {
        _log?.LogDebug("Initialising EliteAPI v{Version}", typeof(EliteDangerousApi).Assembly.GetName().Version);

        // Apply the configuration
        Config.Apply();

        // Use the Localised structs while parsing
        Parser.Use<LocalisedConverter>();

        // Register all events
        Events.Register();

        Events.On<StatusEvent>(HandleStatus);

        // Register Journal files
        _reader.Register(new FileSelector(new DirectoryInfo(Config.JournalsPath), Config.JournalPattern, true));

        // Register status files
        foreach (var statusFile in Config.StatusFiles)
        {
            _reader.Register(new FileSelector(new DirectoryInfo(Config.JournalsPath), statusFile));
        }

        _hasInitialised = true;
    }

    /// <inheritdoc />
    public async Task StartAsync()
    {
        if (!_hasInitialised)
            await InitialiseAsync();

        try
        {
            _log?.LogInformation("Starting EliteAPI v{Version}", typeof(EliteDangerousApi).Assembly.GetName().Version);

            _journalsDirectory = new DirectoryInfo(Config.JournalsPath);
            _optionsDirectory = new DirectoryInfo(Config.OptionsPath);

            var missingDirectories = new[] {_journalsDirectory, _optionsDirectory}.Where(d => !d.Exists).ToList();

            if (missingDirectories.Any())
            {
                var ex = new DirectoryNotFoundException("Could not find necessary Elite: Dangerous directories");
                ex.Data.Add("DirectoryPaths", missingDirectories.Select(d => d.FullName).ToArray());

                _log?.LogError(ex, "Could not find necessary directories. Please check your configuration");
                throw ex;
            }


            IsRunning = true;

            _mainTask = Task.Run(async () =>
            {
                var isFirstRun = true;

                while (IsRunning)
                {
                    await foreach (var (file, line) in _reader.FindNew())
                    {
                        if (string.IsNullOrEmpty(line))
                            continue;

                        var context = new EventContext()
                        {
                            IsRaisedDuringCatchup = isFirstRun,
                            SourceFile = file.FullName
                        };

                        Events.Invoke(line, context);
                    }

                    isFirstRun = false;
                    await Task.Delay(500);
                }
            });
        }
        catch (Exception ex)
        {
            _log?.LogCritical(ex, "Could not start EliteAPI");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task StopAsync()
    {
        IsRunning = false;
        await _mainTask;
    }

    /// <inheritdoc />
    public IEvents Events { get; }


    private StatusEvent? _lastStatus;

    private void HandleStatus(StatusEvent status, EventContext context)
    {
        // Check each property to see if it has changed
        var properties = typeof(StatusEvent).GetProperties();

        foreach (var property in properties)
        {
            if (property.Name is "Timestamp" or "Event" or "Flags" or "Flags2")
                continue;
            
            var oldValue = _lastStatus != null ? property.GetValue(_lastStatus) : null;
            var newValue = property.GetValue(status);

            if (JsonConvert.SerializeObject(oldValue) == JsonConvert.SerializeObject(newValue))
                continue;

            var typeName = $"EliteAPI.Events.Status.Ship.Events.{property.Name}StatusEvent";
            var statusEventType = typeof(GearStatusEvent).Assembly.GetType(typeName);
            if (statusEventType == null)
            {
                _log?.LogWarning("Could not find type {TypeName}", typeName);
                continue;
            }

            var emptyStatusEvent = new EmptyStatusEvent()
            {
                Timestamp = DateTime.Now,
                Event = $"{property.Name}Status",
                Value = newValue
            };

            var json = JsonConvert.SerializeObject(emptyStatusEvent);

            Events.Invoke(json, context);
        }

        _lastStatus = status;
    }
}

class EmptyStatusEvent : IStatusEvent
{
    public DateTime Timestamp { get; init; }

    public string Event { get; init; }

    public object Value { get; init; }
}
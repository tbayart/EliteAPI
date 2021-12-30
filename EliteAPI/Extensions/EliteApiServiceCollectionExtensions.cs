﻿using System;
using EliteAPI;
using EliteAPI.Abstractions;
using EliteAPI.Abstractions.Configuration;
using EliteAPI.Abstractions.Events;
using EliteAPI.Configuration;
using EliteAPI.Events;
using EliteAPI.Extensions;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class EliteDangerousApiServiceCollectionExtensions
{
    /// <summary>Adds EliteAPI services to the specified <see cref="IServiceCollection" />.</summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddEliteApi(this IServiceCollection services)
    {
        return services.AddEliteApi(_ => { });
    }

    /// <summary>Adds EliteAPI services to the specified <see cref="IServiceCollection" />.</summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <param name="configure">The <see cref="EliteDangerousApiOptions" /> configuration delegate.</param>
    /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
    public static IServiceCollection AddEliteApi(this IServiceCollection services,
        Action<EliteDangerousApiOptions> configure)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services), "Service collection cannot be null.");

        if (configure != null)
        {
            // todo: configure the api   
        }

        services.TryAddSingleton<IEliteDangerousApi, EliteDangerousApi>();
        services.TryAddSingleton<IEliteDangerousApiConfiguration, EliteDangerousApiConfiguration>();
        services.TryAddSingleton<IEvents, Events>();
        services.TryAddSingleton<IEventUtilities, EventUtilities>();

        return services;
    }
}
namespace EliteAPI.Events
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    public partial class LaunchFighterInfo : IEvent
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; internal set; }
        [JsonProperty("event")]
        public string Event { get; internal set; }
        [JsonProperty("Loadout")]
        public string Loadout { get; internal set; }
        [JsonProperty("PlayerControlled")]
        public bool PlayerControlled { get; internal set; }
    }
    public partial class LaunchFighterInfo
    {
        internal static LaunchFighterInfo Process(string json, EliteDangerousAPI api) => api.Events.InvokeLaunchFighterEvent(JsonConvert.DeserializeObject<LaunchFighterInfo>(json, EliteAPI.Events.LaunchFighterConverter.Settings));
    }
    
    internal static class LaunchFighterConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore, MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

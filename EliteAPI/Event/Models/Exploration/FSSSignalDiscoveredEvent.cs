using System;

using EliteAPI.Event.Models;
using EliteAPI.Event.Models.Abstractions;

using Newtonsoft.Json;

using ProtoBuf;

namespace EliteAPI.Event.Models
{

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class FssSignalDiscoveredEvent : EventBase<FssSignalDiscoveredEvent>
    {
        internal FssSignalDiscoveredEvent() { }

        [JsonProperty("SystemAddress")]
        public string SystemAddress { get; private set; }

        [JsonProperty("SignalName")]
        public string SignalName { get; private set; }

        [JsonProperty("SignalName_Localised")]
        public string SignalNameLocalised { get; private set; }

        [JsonProperty("IsStation")]
        public bool IsStation { get; private set; }

        [JsonProperty("USSType")]
        public string USSType { get; private set; }

        [JsonProperty("USSType_Localised")]
        public string USSTypeLocalised { get; private set; }

        [JsonProperty("SpawningState")]
        public string SpawningState { get; private set; }

        [JsonProperty("SpawningState_Localised")]
        public string SpawningStateLocalised { get; private set; }

        [JsonProperty("SpawningFaction")]
        public string SpawningFaction { get; private set; }

        [JsonProperty("ThreatLevel")]
        public int ThreatLevel { get; private set; }

        [JsonProperty("TimeRemaining")]
        public double TimeRemaining { get; private set; }
    }

}

namespace EliteAPI.Event.Handler
{
    public partial class EventHandler
    {
        public event EventHandler<FssSignalDiscoveredEvent> FssSignalDiscoveredEvent;

        internal void InvokeFssSignalDiscoveredEvent(FssSignalDiscoveredEvent arg)
        {
            FssSignalDiscoveredEvent?.Invoke(this, arg);
        }
    }
}
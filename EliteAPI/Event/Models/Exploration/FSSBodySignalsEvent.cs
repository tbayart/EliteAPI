using System;
using System.Collections.Generic;
using EliteAPI.Event.Models;
using EliteAPI.Event.Models.Abstractions;

using Newtonsoft.Json;

using ProtoBuf;

namespace EliteAPI.Event.Models
{

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class FSSBodySignalsEvent : EventBase<FSSBodySignalsEvent>
    {
        internal FSSBodySignalsEvent() { }

        [JsonProperty("BodyName")]
        public string BodyName { get; set; }

        [JsonProperty("BodyID")]
        public string BodyId { get; set; }

        [JsonProperty("SystemAddress")]
        public string SystemAddress { get; set; }

        [JsonProperty("Signals")]
        public IReadOnlyList<Signal> Signals { get; private set; }
    }
}

namespace EliteAPI.Event.Handler
{
    public partial class EventHandler
    {
        public event EventHandler<FSSBodySignalsEvent> FSSBodySignalsEvent;

        internal void InvokeFSSBodySignalsEvent(FSSBodySignalsEvent arg)
        {
            FSSBodySignalsEvent?.Invoke(this, arg);
        }
    }
}
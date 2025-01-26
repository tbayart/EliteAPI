using System;

using EliteAPI.Event.Models;
using EliteAPI.Event.Models.Abstractions;

using Newtonsoft.Json;

using ProtoBuf;

namespace EliteAPI.Event.Models
{

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SupercruiseDestinationDropEvent : EventBase<SupercruiseDestinationDropEvent>
    {
        internal SupercruiseDestinationDropEvent() { }

        [JsonProperty("Type")]
        public string Type { get; private set; }

        [JsonProperty("Type_Localised")]
        public string TypeLocalised { get; private set; }

        [JsonProperty("MarketID")]
        public string MarketId { get; private set; }

        [JsonProperty("Threat")]
        public long Threat { get; private set; }
    }

}

namespace EliteAPI.Event.Handler
{
    public partial class EventHandler
    {
        public event EventHandler<SupercruiseDestinationDropEvent> SupercruiseDestinationDropEvent;

        internal void InvokeSupercruiseDestinationDropEvent(SupercruiseDestinationDropEvent arg)
        {
            SupercruiseDestinationDropEvent?.Invoke(this, arg);
        }
    }
}
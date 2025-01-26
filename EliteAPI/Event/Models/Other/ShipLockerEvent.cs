using System;
using System.Collections.Generic;

using EliteAPI.Event.Models;
using EliteAPI.Event.Models.Abstractions;

using Newtonsoft.Json;

using ProtoBuf;

namespace EliteAPI.Event.Models
{

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class ShipLockerEvent : EventBase<ShipLockerEvent>
    {
        internal ShipLockerEvent() { }

        [JsonProperty("Items")]
        public IReadOnlyList<ItemInfo> Items { get; private set; }

        [JsonProperty("Components")]
        public IReadOnlyList<ItemInfo> Components { get; private set; }

        [JsonProperty("Consumables")]
        public IReadOnlyList<ItemInfo> Consumables { get; private set; }

        [JsonProperty("Data")]
        public IReadOnlyList<ItemInfo> Data { get; private set; }


        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class ItemInfo
        {
            internal ItemInfo() { }

            [JsonProperty("Name")]
            public string Name { get; private set; }

            [JsonProperty("Name_Localised")]
            public string NameLocalised { get; private set; }

            [JsonProperty("OwnerID")]
            public string OwnerId { get; private set; }

            [JsonProperty("MissionID", NullValueHandling = NullValueHandling.Ignore)]
            public long MissionId { get; private set; }

            [JsonProperty("Count")]
            public long Count { get; private set; }
        }
    }

}

namespace EliteAPI.Event.Handler
{
    public partial class EventHandler
    {
        public event EventHandler<ShipLockerEvent> ShipLockerEvent;

        internal void InvokeShipLockerEvent(ShipLockerEvent arg)
        {
            ShipLockerEvent?.Invoke(this, arg);
        }
    }
}

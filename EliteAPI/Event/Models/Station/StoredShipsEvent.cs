using System;
using System.Collections.Generic;

using EliteAPI.Event.Models;
using EliteAPI.Event.Models.Abstractions;

using Newtonsoft.Json;

using ProtoBuf;

namespace EliteAPI.Event.Models
{

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class StoredShipsEvent : EventBase<StoredShipsEvent>
    {
        internal StoredShipsEvent() { }

        [JsonProperty("StationName")]
        public string StationName { get; private set; }

        [JsonProperty("MarketID")]
        public string MarketId { get; private set; }

        [JsonProperty("StarSystem")]
        public string StarSystem { get; private set; }

        [JsonProperty("ShipsHere")]
        public IReadOnlyList<ShipsInfo> ShipsHere { get; private set; }

        [JsonProperty("ShipsRemote")]
        public IReadOnlyList<ShipsInfo> ShipsRemote { get; private set; }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class ShipsInfo
    {
        internal ShipsInfo() { }

        [JsonProperty("ShipID")]
        public long ShipId { get; private set; }

        [JsonProperty("ShipType")]
        public string ShipType { get; private set; }

        [JsonProperty("ShipType_Localised", NullValueHandling = NullValueHandling.Ignore)]
        public string ShipTypeLocalised { get; private set; }

        [JsonProperty("Value")]
        public long Value { get; private set; }

        [JsonProperty("Hot")]
        public bool Hot { get; private set; }

        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; private set; }

        [JsonProperty("StarSystem")]
        public string StarSystem { get; private set; }

        [JsonProperty("ShipMarketID")]
        public string ShipMarketId { get; private set; }

        [JsonProperty("TransferPrice")]
        public int TransferPrice { get; private set; }

        [JsonProperty("TransferTime")]
        public int TransferTime { get; private set; }
    }

}

namespace EliteAPI.Event.Handler
{
    public partial class EventHandler
    {
        public event EventHandler<StoredShipsEvent> StoredShipsEvent;

        internal void InvokeStoredShipsEvent(StoredShipsEvent arg)
        {
            StoredShipsEvent?.Invoke(this, arg);
        }
    }
}
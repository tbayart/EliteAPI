using System;
using System.Collections.Generic;

using EliteAPI.Event.Models;
using EliteAPI.Event.Models.Abstractions;

using Newtonsoft.Json;

using ProtoBuf;

namespace EliteAPI.Event.Models
{

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class StoredModulesEvent : EventBase<StoredModulesEvent>
    {
        internal StoredModulesEvent() { }

        [JsonProperty("MarketID")]
        public string MarketId { get; private set; }

        [JsonProperty("StationName")]
        public string StationName { get; private set; }

        [JsonProperty("StarSystem")]
        public string StarSystem { get; private set; }

        [JsonProperty("Items")]
        public IReadOnlyList<Item> Items { get; private set; }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class Item
    {
        internal Item() { }

        [JsonProperty("Name")]
        public string Name { get; private set; }

        [JsonProperty("Name_Localised")]
        public string NameLocalised { get; private set; }

        [JsonProperty("StorageSlot")]
        public long StorageSlot { get; private set; }

        [JsonProperty("StarSystem")]
        public string StarSystem { get; private set; }

        [JsonProperty("MarketID")]
        public long MarketId { get; private set; }

        [JsonProperty("TransferCost")]
        public long TransferCost { get; private set; }

        [JsonProperty("TransferTime")]
        public long TransferTime { get; private set; }

        [JsonProperty("BuyPrice")]
        public long BuyPrice { get; private set; }

        [JsonProperty("Hot")]
        public bool Hot { get; private set; }

        [JsonProperty("EngineerModifications", NullValueHandling = NullValueHandling.Ignore)]
        public string EngineerModifications { get; private set; }

        [JsonProperty("Level", NullValueHandling = NullValueHandling.Ignore)]
        public long? Level { get; private set; }

        [JsonProperty("Quality", NullValueHandling = NullValueHandling.Ignore)]
        public double? Quality { get; private set; }

        [JsonProperty("InTransit")]
        public bool IsInTransit { get; private set; }
    }

}

namespace EliteAPI.Event.Handler
{
    public partial class EventHandler
    {
        public event EventHandler<StoredModulesEvent> StoredModulesEvent;

        internal void InvokeStoredModulesEvent(StoredModulesEvent arg)
        {
            StoredModulesEvent?.Invoke(this, arg);
        }
    }
}
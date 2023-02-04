using System;

using EliteAPI.Event.Models;
using EliteAPI.Event.Models.Abstractions;

using Newtonsoft.Json;

using ProtoBuf;

namespace EliteAPI.Event.Models
{

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class ModuleRetrieveEvent : EventBase<ModuleRetrieveEvent>
    {
        internal ModuleRetrieveEvent() { }

        [JsonProperty("MarketID")]
        public string MarketId { get; private set; }

        [JsonProperty("Slot")]
        public string Slot { get; private set; }

        [JsonProperty("RetrievedItem")]
        public string RetrievedItem { get; private set; }

        [JsonProperty("RetrievedItem_Localised")]
        public string RetrievedItemLocalised { get; private set; }

        [JsonProperty("Ship")]
        public string Ship { get; private set; }

        [JsonProperty("ShipID")]
        public string ShipId { get; private set; }

        [JsonProperty("Hot")]
        public bool IsHot { get; private set; }

        [JsonProperty("EngineerModifications")]
        public string EngineerModifications { get; private set; }

        [JsonProperty("Level")]
        public int Level { get; private set; }

        [JsonProperty("Quality")]
        public decimal Quality { get; private set; }

        [JsonProperty("SwapOutItem")]
        public string SwapOutItem { get; private set; }

        [JsonProperty("SwapOutItem_Localised")]
        public string SwapOutItemLocalised { get; private set; }
    }
}

namespace EliteAPI.Event.Handler
{
    public partial class EventHandler
    {
        public event EventHandler<ModuleRetrieveEvent> ModuleRetrieveEvent;

        internal void InvokeModuleRetrieveEvent(ModuleRetrieveEvent arg)
        {
            ModuleRetrieveEvent?.Invoke(this, arg);
        }
    }
}
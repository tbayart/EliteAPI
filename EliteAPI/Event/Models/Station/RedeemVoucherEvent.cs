using System;
using System.Collections.Generic;
using EliteAPI.Event.Models;
using EliteAPI.Event.Models.Abstractions;

using Newtonsoft.Json;

using ProtoBuf;

namespace EliteAPI.Event.Models
{

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class RedeemVoucherEvent : EventBase<RedeemVoucherEvent>
    {
        internal RedeemVoucherEvent() { }

        [JsonProperty("Type")]
        public string Type { get; private set; }

        [JsonProperty("Amount")]
        public long Amount { get; private set; }

        [JsonProperty("Faction")]
        public string Faction { get; private set; }

        [JsonProperty("Factions")]
        public IReadOnlyList<FactionVoucher> Factions { get; private set; }

        [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
        public class FactionVoucher
        {
            internal FactionVoucher() { }

            [JsonProperty("Amount")]
            public int Amount { get; private set; }

            [JsonProperty("Faction")]
            public string Faction { get; private set; }
        }
    }
}

namespace EliteAPI.Event.Handler
{
    public partial class EventHandler
    {
        public event EventHandler<RedeemVoucherEvent> RedeemVoucherEvent;

        internal void InvokeRedeemVoucherEvent(RedeemVoucherEvent arg)
        {
            RedeemVoucherEvent?.Invoke(this, arg);
        }
    }
}
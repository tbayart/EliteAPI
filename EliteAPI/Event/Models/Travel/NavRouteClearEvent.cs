using System;

using EliteAPI.Event.Models;
using EliteAPI.Event.Models.Abstractions;

using Newtonsoft.Json;

using ProtoBuf;

namespace EliteAPI.Event.Models
{

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class NavRouteClearEvent : EventBase<NavRouteClearEvent>
    {
        internal NavRouteClearEvent() { }
    }
}

namespace EliteAPI.Event.Handler
{
    public partial class EventHandler
    {
        public event EventHandler<NavRouteClearEvent> NavRouteClearEvent;

        internal void InvokeNavRouteClearEvent(NavRouteClearEvent arg)
        {
            NavRouteClearEvent?.Invoke(this, arg);
        }
    }
}

using System;

using EliteAPI.Event.Models;
using EliteAPI.Event.Models.Abstractions;

using Newtonsoft.Json;

using ProtoBuf;

namespace EliteAPI.Event.Models
{

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class NavRouteEvent : EventBase<NavRouteEvent>
    {
        internal NavRouteEvent() { }
    }
}

namespace EliteAPI.Event.Handler
{
    public partial class EventHandler
    {
        public event EventHandler<NavRouteEvent> NavRouteEvent;

        internal void InvokeNavRouteEventEvent(NavRouteEvent arg)
        {
            NavRouteEvent?.Invoke(this, arg);
        }
    }
}

using Commander.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commander.Core
{
    public class DomainEvent : Event
    {
        public DomainEvent(IEvent @event)
        {
            Event = @event;
        }

        public IEvent Event { get; set; }
    }
}

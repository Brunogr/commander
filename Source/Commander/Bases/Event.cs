using System;
using System.Collections.Generic;
using System.Text;

namespace Commander.Abstractions.Bases
{
    public abstract class Event : IEvent
    {
        protected Event()
        {
            Timestamp = DateTime.Now;
            MessageType = GetType().Name;
        }

        public DateTime Timestamp { get; private set; }

        public string MessageType { get; private set; }
    }
}

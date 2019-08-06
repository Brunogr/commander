using System;
using System.Collections.Generic;
using System.Text;

namespace Commander.Abstractions
{
    public interface IAggregateRoot
    {
        List<IEvent> Events { get; }
        void ClearEvents();
    }
}

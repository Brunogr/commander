using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commander.Abstractions.Bases
{
    public abstract class EventHandler<TEvent> : IEventHandler<TEvent> where TEvent : Event
    {
        public async Task Handle(TEvent notification, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            await HandleEvent(notification);
        }

        public abstract Task HandleEvent(TEvent @event);
    }
}

using Commander.Abstractions;
using Commander.Core.Base;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commander.Core
{
    public abstract class DomainEventHandler<TEvent> : HandlerBase, IEventHandler<TEvent> where TEvent : IEvent
    {
        public DomainEventHandler(IHandler handler) : base(handler)
        {
        }

        public async Task Handle(TEvent notification, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            await HandleEvent(notification);

            if (Invalid)
                await Notify(Notifications);

            //If there is an event of the same type of the event being processed, 
            //it'll be removed to avoid infinite loop.
            if (events.Any(ev => ev is TEvent))
                events.Remove(events.FirstOrDefault(ev => ev is TEvent));

            await RaiseEvents();
        }

        public abstract Task HandleEvent(TEvent @event);
    }
}

using Commander.Abstractions;
using Commander.MessageBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commander.Core
{
    public class DomainEventFactoryHandler : DomainEventHandler<DomainEvent>
    {
        private readonly IMessageBus messageBus;
        private readonly IHandler handler;

        public DomainEventFactoryHandler(IMessageBus messageBus, IHandler handler) : base(handler)
        {
            this.messageBus = messageBus ?? null;
            this.handler = handler;
        }
        public async override Task HandleEvent(DomainEvent @event)
        {
            if (@event.Event is IEventAsync)
            {
                if (messageBus == null)
                    throw new NotImplementedException("To use Async Events, please provide an implementation for IMessageBus.");

                await this.messageBus.PublishAsync(@event.Event);
            }
            else
                await handler.RaiseEvent(@event.Event);
        }
    }
}

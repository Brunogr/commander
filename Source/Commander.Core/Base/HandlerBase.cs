using Commander.Abstractions;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander.Core.Base
{
    public class HandlerBase : Notifiable
    {
        private readonly IHandler handler;

        protected List<IEvent> events;
        public HandlerBase(IHandler handler)
        {
            this.handler = handler;
        }
        protected void HandleEntity<TEntity>(TEntity entity) where TEntity : Notifiable
        {
            if (entity is IAggregateRoot aggregateRoot)
            {
                events.AddRange(aggregateRoot.Events);
                aggregateRoot.ClearEvents();
            }
            if (entity is Notifiable)
                AddNotifications(entity.Notifications);
        }

        protected async Task RaiseEvents()
        {
            if (events.Any())
            {
                List<Task> eventsTasks = new List<Task>();

                foreach (var @event in events)
                {
                    var domainEvent = new DomainEvent(@event);
                    eventsTasks.Add(handler.RaiseEvent(domainEvent));
                }

                await Task.WhenAll(eventsTasks);
            }
        }

        protected Task Notify(IReadOnlyCollection<Notification> notifications)
        {
            foreach (var error in notifications)
            {
                handler.RaiseEvent(new DomainNotification(error.Property, error.Message));
            }

            return Task.CompletedTask;
        }

    }
}

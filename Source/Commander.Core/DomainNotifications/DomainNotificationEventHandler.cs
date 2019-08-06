using Commander.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commander.Core.DomainNotifications
{
    public class DomainNotificationEventHandler : DomainEventHandler<DomainNotification>
    {
        private IDomainNotificationService domainNotificationService;

        public DomainNotificationEventHandler(IHandler handler, IDomainNotificationService domainNotificationService) : base(handler)
        {
            this.domainNotificationService = domainNotificationService;
        }

        public override async Task HandleEvent(DomainNotification @event)
        {
            await domainNotificationService.AddNotificationAsync(@event);
        }
    }
}

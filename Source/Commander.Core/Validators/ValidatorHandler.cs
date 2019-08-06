using Commander.Abstractions;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commander.Core.Validators
{
    public abstract class ValidatorHandler<TMessage> : Notifiable, IValidatorHandler<TMessage>
        where TMessage : IValidatable
    {
        readonly IDomainNotificationService _domainNotificationService;

        public ValidatorHandler(IDomainNotificationService domainNotificationService)
        {
            _domainNotificationService = domainNotificationService;
        }
        public async Task Handle(TMessage notification, CancellationToken cancellationToken)
        {
            var isValid = await ValidateAsync(notification);

            if (!isValid)
            {
                foreach (var validationNotification in this.Notifications)
                {
                    await _domainNotificationService.AddNotificationAsync(new DomainNotification(validationNotification.Property, validationNotification.Message));
                }
            }
        }

        protected abstract Task<bool> ValidateAsync(TMessage message);

        public virtual bool Required { get { return true; } }
    }
}

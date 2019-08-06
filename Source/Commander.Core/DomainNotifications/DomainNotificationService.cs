using Commander.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander.Core.DomainNotifications
{
    public class DomainNotificationService : IDomainNotificationService
    {

        private readonly List<DomainNotification> notifications;

        public DomainNotificationService()
        {
            notifications = new List<DomainNotification>();
        }

        public List<DomainNotification> GetNotifications()
        {
            return notifications;
        }
        public bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public void AddNotification(DomainNotification notification)
        {
            notifications.Add(notification);
        }

        public void AddNotifications(params DomainNotification[] notifications)
        {
            this.notifications.AddRange(notifications);
        }

        public async Task AddNotificationAsync(DomainNotification notification)
        {
            await Task.Factory.StartNew(() =>
            {
                AddNotification(notification);
            });
        }

        public async Task AddNotificationAsync(params DomainNotification[] notifications)
        {
            await Task.Factory.StartNew(() =>
            {
                AddNotifications(notifications);
            });
        }

        public Task<List<DomainNotification>> GetNotificationsAsync()
        {
            return Task.FromResult(GetNotifications());
        }

        public Task<bool> HasNotificationsAsync()
        {
            return Task.FromResult(HasNotifications());
        }

        public void Clear()
        {
            notifications.Clear();
        }

        public async Task ClearAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                Clear();
            });
        }
    }
}

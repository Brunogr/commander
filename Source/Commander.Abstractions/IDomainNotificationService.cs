using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commander.Abstractions
{
    public interface IDomainNotificationService
    {
        Task AddNotificationAsync(DomainNotification notification);
        Task AddNotificationAsync(params DomainNotification[] notifications);
        Task<List<DomainNotification>> GetNotificationsAsync();
        Task<bool> HasNotificationsAsync();
        Task ClearAsync();
    }
}

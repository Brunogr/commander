using System;
using System.Collections.Generic;
using System.Text;

namespace Commander.Abstractions
{
    public class DomainNotification : IEvent
    {
        public string Key { get; private set; }
        public string Value { get; private set; }
        public NotificationType Type { get; private set; }
        public string MessageType => GetType().Name;
        string IMessage.MessageType { get => GetType().Name; }

        DateTimeOffset IMessage.Timestamp { get; }

        public DomainNotification(string key, string value, NotificationType type = NotificationType.Error)
        {
            Key = key;
            Value = value;
            Type = type;
        }
    }

    public enum NotificationType
    {
        Success = 1,
        Alert,
        Error,
        Info
    }
}

using Commander.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Commander.MessageBus.Abstractions
{
    public interface IMessageBus
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : IMessage;

        Task SubscribeAsync<TMessage>() where TMessage : IMessage;

        Task SubscribeAsync<TMessage>(string queue) where TMessage : Commander.Abstractions.IMessage;

        Task<TMessage> ReceiveAsync<TMessage>(CancellationToken cancellationToken);
    }
}

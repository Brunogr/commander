using Commander.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Commander.MessageBus.Abstractions
{
    public interface IMessageBus
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : IMessage;

        Task SubscribeAsync<TMessage>(CancellationToken cancellationToken = default(CancellationToken)) where TMessage : IMessage;

        //Task SubscribeAsync<TMessage>(string queue) where TMessage : IMessage;

        Task<TMessage> ReceiveAsync<TMessage>(CancellationToken cancellationToken) where TMessage : IMessage;

        Task<List<TMessage>> ReceiveMultipleAsync<TMessage>(CancellationToken cancellationToken) where TMessage : IMessage;

        Task DeleteAsync<TMessage>(TMessage message) where TMessage : IMessage;
    }
}

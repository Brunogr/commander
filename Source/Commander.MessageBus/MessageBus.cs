using Commander.Abstractions;
using Commander.MessageBus.Abstractions;
using Newtonsoft.Json;
using Otc.PubSub.Abstractions;
using Otc.PubSub.PunchySubscriber.Abstractions;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commander.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly IHandler handler;
        private readonly ISubscriber subscriber;
        private readonly IPubSub pubSub;
        private readonly IMessageBusConfiguration configuration;
        public MessageBus(IHandler handler, IMessageBusConfiguration configuration, 
            ISubscriber subscriber, IPubSub pubsub)
        {
            this.handler = handler;
            this.configuration = configuration;
            this.subscriber = subscriber;
            this.pubSub = pubsub;
        }

        public async Task PublishAsync<TMessage>(TMessage message) where TMessage : Commander.Abstractions.IMessage
        {
            var json = JsonConvert.SerializeObject(message);
            await this.pubSub.PublishAsync(message.MessageType, Encoding.UTF8.GetBytes(json));
        }

        public Task<TMessage> ReceiveAsync<TMessage>(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SubscribeAsync<TMessage>(string queue) where TMessage : Commander.Abstractions.IMessage
        {
            await this.subscriber.SubscribeAsync(async (PunchyMessage message) =>
            {
                await HandleMessage<TMessage>(message);
            },
           this.configuration.GroupId,
           new CancellationToken(),
           queue);
        }

        public async Task SubscribeAsync<TMessage>() where TMessage : Commander.Abstractions.IMessage
        {
            var topicName = typeof(TMessage).Name;

            await this.SubscribeAsync<TMessage>(topicName);
        }

        private async Task HandleMessage<TMessage>(PunchyMessage message) where TMessage : Commander.Abstractions.IMessage
        {
            var json = Encoding.Default.GetString(message.MessageBytes);
            var messageDeserialized = JsonConvert.DeserializeObject<TMessage>(json);

            if (messageDeserialized is ICommand<CommandResult>)
            {
                await handler.Send((ICommand<CommandResult>)messageDeserialized);
            }
            else
                await handler.RaiseEvent((IEvent)messageDeserialized);
        }
    }
}

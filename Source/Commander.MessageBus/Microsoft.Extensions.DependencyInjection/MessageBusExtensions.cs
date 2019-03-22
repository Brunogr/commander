using Commander.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MessageBusExtensions
    {
        public static IServiceCollection AddMessageBusKafka(this IServiceCollection serviceCollection, Action<MessageBusConfigurationLambda> configuration, 
            params Assembly[] assemblies)
        {
            serviceCollection.AddPunchySubscriber(config => 
                config.Configure(
                    new Otc.PubSub.PunchySubscriber.SubscriberConfiguration() { LevelDelaysInSeconds = [30, 60, 90, 120] }
                )
            );

            var configLambda = new MessageBusConfigurationLambda(serviceCollection);

            configuration.Invoke(configLambda);

            serviceCollection.AddCommander(assemblies);

            return serviceCollection;
        }
    }
}

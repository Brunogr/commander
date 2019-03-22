using Commander.MessageBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commander.MessageBus
{
    public class MessageBusConfiguration : IMessageBusConfiguration
    {
        public MessageBusConfiguration(string connectionString, string groupId)
        {
            ConnectionString = connectionString;
            GroupId = groupId;
        }

        public string ConnectionString { get; private set; }

        public string GroupId { get; private set; }
    }

    public class MessageBusConfigurationLambda
    {
        public MessageBusConfigurationLambda(IServiceCollection services)
        {
            this.services = services;
        }
        public MessageBusConfiguration configuration;

        private readonly IServiceCollection services;

        public MessageBusConfigurationLambda Configure(MessageBusConfiguration configuration)
        {
            services.AddSingleton<IMessageBusConfiguration>(configuration);

            return this;
        }
    }
}

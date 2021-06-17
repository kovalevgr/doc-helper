using System;
using DocHelper.Infrastructure.MessageBrokers.Kafka;
using DocHelper.Infrastructure.MessageBrokers.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Infrastructure.MessageBrokers
{
    public static class MessageBrokersExtensions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new MessageBrokersOptions();
            configuration.GetSection(nameof(MessageBrokersOptions)).Bind(options);
            services.Configure<MessageBrokersOptions>(configuration.GetSection(nameof(MessageBrokersOptions)));

            switch (options.MessageBrokerType.ToLowerInvariant())
            {
                case "rabbitmq":
                    return services.AddRabbitMQ(configuration);
                case "kafka":
                    return services.AddKafka(configuration);
                default:
                    throw new Exception($"Message broker type '{options.MessageBrokerType}' is not supported");
            }
        }
    }
}
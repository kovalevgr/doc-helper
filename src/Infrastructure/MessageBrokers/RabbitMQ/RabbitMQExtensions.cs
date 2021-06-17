using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Instantiation;

namespace DocHelper.Infrastructure.MessageBrokers.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMQOptions();
            configuration.GetSection(nameof(MessageBrokersOptions)).Bind(options);
            services.Configure<RabbitMQOptions>(configuration.GetSection(nameof(MessageBrokersOptions)));

            services.AddRawRabbit(new RawRabbitOptions
            {
                ClientConfiguration = options
            });

            services.AddSingleton<IEventListener, RabbitMQListener>();

            return services;
        }
    }
}
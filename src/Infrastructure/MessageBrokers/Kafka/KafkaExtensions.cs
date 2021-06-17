using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Infrastructure.MessageBrokers.Kafka
{
    public static class KafkaExtensions
    {
        public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new KafkaOptions();
            configuration.GetSection(nameof(MessageBrokersOptions)).Bind(options);
            services.Configure<KafkaOptions>(configuration.GetSection(nameof(MessageBrokersOptions)));

            services.AddSingleton<IEventListener, KafkaListener>();

            return services;
        }
    }
}
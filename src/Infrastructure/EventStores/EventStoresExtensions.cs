using System;
using DocHelper.Infrastructure.EventStores.Stores.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Infrastructure.EventStores
{
    public static class EventStoresExtensions
    {
        public static IServiceCollection AddEventStore(this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new EventStoresOptions();
            configuration.GetSection(nameof(EventStoresOptions)).Bind(options);
            services.Configure<EventStoresOptions>(configuration.GetSection(nameof(EventStoresOptions)));

            services.AddScoped<IEventStore, EventStore>();

            switch (options.EventStoreType.ToLowerInvariant())
            {
                case "redis":
                    services.AddRedisEventStore(configuration);
                    break;
                default:
                    throw new Exception($"Event store type '{options.EventStoreType}' is not supported");
            }


            return services;
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Infrastructure.EventStores
{
    public static class EventStoresExtensions
    {
        public static IServiceCollection AddEventStore(this IServiceCollection services, IConfiguration configuration, Action<DbContextOptionsBuilder> dbContextOptions = null)
        {
            var options = new EventStoresOptions();
            configuration.GetSection(nameof(EventStoresOptions)).Bind(options);
            services.Configure<EventStoresOptions>(configuration.GetSection(nameof(EventStoresOptions)));

            switch (options.EventStoreType.ToLowerInvariant())
            {
                default:
                    throw new Exception($"Event store type '{options.EventStoreType}' is not supported");
            }

            services.AddScoped<IEventStore, EventStore>();

            return services;
        }
    }
}
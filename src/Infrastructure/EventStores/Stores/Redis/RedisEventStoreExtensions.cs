using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Infrastructure.EventStores.Stores.Redis
{
    public static class RedisEventStoreExtensions
    {
        public static IServiceCollection AddRedisEventStore(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IStore, RedisEventStore>();
            
            return services;
        }
    }
}
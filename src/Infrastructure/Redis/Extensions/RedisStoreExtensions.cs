using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace DocHelper.Infrastructure.Redis.Extensions
{
    public static class RedisStoreExtensions
    {
        public static IServiceCollection AddRedisExtensions(this IServiceCollection services,
            IConfiguration configuration)
        {
            var redisConfiguration = configuration.GetSection("Redis").Get<RedisConfiguration>();

            services.AddSingleton(redisConfiguration);

            services.AddSingleton<IRedisCacheClient, RedisCacheClient>();
            services.AddSingleton<IRedisCacheClient, RedisCacheClient>();
            services.AddSingleton<IRedisCacheConnectionPoolManager, RedisCacheConnectionPoolManager>();
            services.AddSingleton<ISerializer, SystemTextJsonSerializer>();

            services.AddSingleton((provider) =>
                provider.GetRequiredService<IRedisCacheClient>().GetDbFromConfiguration());

            return services;
        }
    }
}
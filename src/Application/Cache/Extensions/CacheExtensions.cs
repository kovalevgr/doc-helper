using DocHelper.Application.Cache.Providers.Configuration;
using DocHelper.Application.Cache.Providers.InMemory;
using DocHelper.Domain.Cache;
using DocHelper.Domain.Cache.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Application.Cache.Extensions
{
    public static class CacheExtensions
    {
        public static IServiceCollection UseInMemoryCache(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new InMemoryOptions();
            configuration.GetSection("CacheOptions:InMemoryOptions").Bind(options);
            services.Configure<InMemoryOptions>(configuration.GetSection("CacheOptions:InMemoryOptions"));

            services.AddSingleton<IInMemoryCaching, InMemoryCaching>();
            services.AddSingleton<ICacheProvider, InMemoryCacheProvider>();

            return services;
        }
    }
}
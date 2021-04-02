using DocHelper.Application.Cache.Providers.Configuration;
using DocHelper.Application.Cache.Providers.InMemory;
using DocHelper.Domain.Cache;
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
            OptionsConfiguration(services, configuration);

            services.AddSingleton<ICacheProvider, InMemoryCacheProvider>();

            return services;
        }

        private static void OptionsConfiguration(
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<CacheProviderOptions>(_ => configuration.GetSection("Caching"));
        }
    }
}
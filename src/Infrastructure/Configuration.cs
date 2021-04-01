using System.Collections.Generic;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Domain.Repository;
using DocHelper.Infrastructure.Cache.Configuration;
using DocHelper.Infrastructure.Cache.Policy;
using DocHelper.Infrastructure.Cache.Processor;
using DocHelper.Infrastructure.Persistence;
using DocHelper.Infrastructure.Persistence.Interceptors;
using DocHelper.Infrastructure.Repository;
using DocHelper.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Infrastructure
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Configure cache
            services.AddTransient<CacheInterceptor>();
            services.AddTransient<ICacheProcessor, CacheProcessor>();
            services.AddTransient<CachePolicyManager>();
            
            services.Configure<CacheOptions>(_ => configuration.GetSection("Caching"));
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                    .AddInterceptors(GetInterceptors(services))
                );

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddSingleton<ILocationService, LocationService>();

            // Repo section
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ISpecialtyRepository, SpecialtyRepository>();

            return services;
        }

        private static IEnumerable<IInterceptor> GetInterceptors(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            
            IList<IInterceptor> interceptors = new List<IInterceptor>();
            
            interceptors.Add(serviceProvider.GetService<CacheInterceptor>());
            
            return interceptors;
        }
    }
}
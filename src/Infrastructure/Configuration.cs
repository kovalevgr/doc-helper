using System.Collections.Generic;
using DocHelper.Domain.Common.Interfaces;
using DocHelper.Domain.Common.Services;
using DocHelper.Domain.Pipeline;
using DocHelper.Domain.Repository;
using DocHelper.Infrastructure.Cache.Configuration;
using DocHelper.Infrastructure.Cache.Policy;
using DocHelper.Infrastructure.Cache.Processor;
using DocHelper.Infrastructure.EventStores;
using DocHelper.Infrastructure.Persistence;
using DocHelper.Infrastructure.Persistence.Interceptors;
using DocHelper.Infrastructure.Pipeline.Builder;
using DocHelper.Infrastructure.Pipeline.Executor;
using DocHelper.Infrastructure.Pipeline.Extension;
using DocHelper.Infrastructure.Redis.Extensions;
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
            // Cache section
            configuration.GetSection(nameof(CacheOptions)).Bind(new CacheOptions());
            services.Configure<CacheOptions>(configuration.GetSection(nameof(CacheOptions)));

            services.AddScoped<CacheInterceptor>();
            services.AddSingleton<ICacheProcessor, CacheProcessor>();
            services.AddSingleton<CachePolicyManager>();

            // DB section
            services.AddDbContextPool<ApplicationDbContext>(
                options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                        .AddInterceptors(GetInterceptors(services))
            );

            services.AddDbContext<DocumentDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultDocumentConnection")));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IApplicationDbTransaction, ApplicationDbTransaction>();

            services.AddSingleton<ILocationService, LocationService>();

            services.AddEventStore(configuration);

            services.AddRedisExtensions(configuration);

            // Repo section
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ISpecialtyRepository, SpecialtyRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();

            services.AddScoped<PipelineBuilder>();
            services.AddScoped<IPipelineExecutor, PipelineExecutor>();

            services.ConfigurePipelines();

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
using System.Reflection;
using DocHelper.Application.Cache.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Application
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.UseInMemoryCache(configuration);

            return services;
        }
    }
}
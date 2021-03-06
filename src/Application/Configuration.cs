using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Application
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
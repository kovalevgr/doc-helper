using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Infrastructure.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new SwaggerOptions();
            configuration.GetSection(nameof(SwaggerOptions)).Bind(options);
            services.Configure<SwaggerOptions>(configuration.GetSection(nameof(SwaggerOptions)));

            if (string.IsNullOrWhiteSpace(options.Title))
            {
                options.Title = AppDomain.CurrentDomain.FriendlyName.Trim().Trim('_');
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.VersionName, options);
                c.CustomSchemaIds(x => x.FullName);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerApp(this IApplicationBuilder app, IConfiguration configuration)
        {
            var options = new SwaggerOptions();
            configuration.GetSection(nameof(SwaggerOptions)).Bind(options);

            if (string.IsNullOrWhiteSpace(options.Title))
            {
                options.Title = AppDomain.CurrentDomain.FriendlyName.Trim().Trim('_');
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{options.VersionName}/swagger.json", options.Title);
                c.RoutePrefix = options.RoutePrefix;
            });

            return app;
        }
    }
}
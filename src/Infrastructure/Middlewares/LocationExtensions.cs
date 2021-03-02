using System;
using System.Threading.Tasks;
using DocHelper.Application.Common.Interfaces;
using DocHelper.Domain.ValueObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace DocHelper.Infrastructure.Middlewares
{
    public static class LocationExtensions
    {
        public static IApplicationBuilder UseLocation(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            
            return app.UseMiddleware<LocationMiddleware>();
        }
    }

    public class LocationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILocationService _locationService;

        public LocationMiddleware(RequestDelegate next, ILocationService locationService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _locationService = locationService;
        }

        public async Task Invoke(HttpContext context)
        {
            bool result = context.Request.Headers.TryGetValue("Location", out StringValues location);
            if (result)
            {
                _locationService.Location = new Location()
                {
                    CurrentLocation = location.ToString()
                };
            }
            
            await _next(context);
        }
    }
}
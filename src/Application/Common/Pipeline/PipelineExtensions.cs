using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DocHelper.Domain.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Application.Common.Pipeline
{
    public static class PipelineExtensions
    {
        public static IServiceCollection ConfigurePipelines(this IServiceCollection services)
        {
            foreach (var stepType in GetSteps())
            {
                services.AddScoped(typeof(IPipelineStep), stepType);
            }

            return services;
        }

        private static IEnumerable<Type> GetSteps()
        {
            return Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineStep)))
                .ToList();
        }
    }
}
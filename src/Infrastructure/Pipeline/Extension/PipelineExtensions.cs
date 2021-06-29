using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DocHelper.Domain.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Infrastructure.Pipeline.Extension
{
    public static class PipelineExtensions
    {
        public static IServiceCollection ConfigurePipelines(this IServiceCollection services)
        {
            foreach (var pipeline in GetPipelines())
            {
                services.AddScoped(typeof(BasePipeline), pipeline);
            }

            foreach (var stepType in GetSteps())
            {
                services.AddScoped(typeof(IPipelineStep), stepType);
            }

            return services;
        }

        private static IEnumerable<Type> GetPipelines()
        {
            return Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BasePipeline)))
                .ToList();
        }

        private static IEnumerable<Type> GetSteps()
        {
            return Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(x => typeof(IPipelineStep).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DocHelper.Domain.Common.Extensions;
using DocHelper.Domain.Graph.Types;
using Microsoft.Extensions.DependencyInjection;

namespace DocHelper.Infrastructure.GraphQL.Extensions
{
    public static class GraphQLServerExtensions
    {
        public static IServiceCollection AddGraphServer<TQuery>(this IServiceCollection services)
            where TQuery : class
        {
            services.AddScoped<TQuery>();

            var builder = services.AddGraphQLServer()
                .AddQueryType<TQuery>()
                // .AddSubscriptionType<TSubscription>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();

            foreach (var type in GetTypes())
            {
                builder.AddType(type);
            }

            return services;
        }

        private static List<Type> GetTypes() =>
            AppDomain.CurrentDomain.GetDomainAssembly()
                .GetExportedTypes()
                .Where(x => typeof(IType).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();
    }
}
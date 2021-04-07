#nullable enable
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DocHelper.Domain.Cache.Policy;
using DocHelper.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DocHelper.Infrastructure.Cache.Query
{
    public static class CacheQueryExtensions
    {
        private static readonly TimeSpan ThirtyMinutes = TimeSpan.FromMinutes(30);

        private static readonly MethodInfo? AsNoTrackingMethodInfo =
            typeof(EntityFrameworkQueryableExtensions)
                .GetTypeInfo()
                .GetDeclaredMethod(nameof(EntityFrameworkQueryableExtensions.AsNoTracking));

        public static readonly string IsCachableMarker = $"{nameof(CachePolicy)}";
        public static readonly string IsNotCachableMarker = $"{nameof(CachePolicy)}{nameof(NotCacheable)}";

        /// <summary>
        /// Returns a new query where the entities returned will be cached.
        /// </summary>
        /// <typeparam name="TType">Entity type.</typeparam>
        /// <param name="query">The input query.</param>
        /// <param name="expirationMode">Defines the expiration mode of the cache item.</param>
        /// <param name="timeout">The expiration timeout.</param>
        /// <returns>Provides functionality to evaluate queries against a specific data source.</returns>
        public static IQueryable<TType> Cacheable<TType>(
            this IQueryable<TType> query,
            CacheExpirationMode expirationMode,
            TimeSpan timeout)
        {
            SanityCheck(query);
            return query.MarkAsNoTracking().TagWith(CachePolicy.Configure(options =>
                options
                    .ExpirationMode(expirationMode)
                    .Timeout(timeout)));
        }

        /// <summary>
        /// Returns a new query where the entities returned will be cached.
        /// </summary>
        /// <typeparam name="TType">Entity type.</typeparam>
        /// <param name="query">The input query.</param>
        /// <param name="expirationMode">Defines the expiration mode of the cache item.</param>
        /// <param name="timeout">The expiration timeout.</param>
        /// Set this option to the `real` related table names of the current query, if you are using an stored procedure,
        /// otherswise cache dependencies of normal queries will be calculated automatically.
        /// This array will be used to invalidate the related cache of all related queries automatically.
        /// </param>
        /// <param name="saltKey">If you think the computed hash of the query to calculate the cache-key is not enough, set this value.</param>
        /// <returns>Provides functionality to evaluate queries against a specific data source.</returns>
        public static IQueryable<TType> Cacheable<TType>(
            this IQueryable<TType> query,
            CacheExpirationMode expirationMode,
            TimeSpan timeout,
            string saltKey)
        {
            SanityCheck(query);
            return query.MarkAsNoTracking().TagWith(CachePolicy.Configure(options =>
                options
                    .ExpirationMode(expirationMode)
                    .Timeout(timeout)
                    .SaltKey(saltKey)));
        }

        /// <summary>
        /// Returns a new query where the entities returned by it will be cached only for 30 minutes.
        /// </summary>
        /// <typeparam name="TType">Entity type.</typeparam>
        /// <param name="query">The input EF query.</param>
        /// <returns>Provides functionality to evaluate queries against a specific data source.</returns>
        public static IQueryable<TType> Cacheable<TType>(
            this IQueryable<TType> query)
        {
            SanityCheck(query);
            return query.MarkAsNoTracking().TagWith(CachePolicy.Configure(options =>
                options.ExpirationMode(CacheExpirationMode.Absolute).Timeout(ThirtyMinutes)
                    .DefaultCacheableMethod(true)));
        }

        /// <summary>
        /// Returns a new query where the entities returned by it will be cached only for 30 minutes.
        /// </summary>
        /// <typeparam name="TType">Entity type.</typeparam>
        /// <param name="query">The input query.</param>
        /// <returns>Provides functionality to evaluate queries against a specific data source.</returns>
        public static IQueryable<TType> Cacheable<TType>(
            this DbSet<TType> query) where TType : class
        {
            SanityCheck(query);
            return query.MarkAsNoTracking().TagWith(CachePolicy.Configure(options =>
                options
                    .ExpirationMode(CacheExpirationMode.Absolute)
                    .Timeout(ThirtyMinutes)
                    .DefaultCacheableMethod(true)));
        }

        /// <summary>
        /// Returns a new query where the entities returned will be cached.
        /// </summary>
        /// <typeparam name="TType">Entity type.</typeparam>
        /// <param name="query">The input query.</param>
        /// <param name="expirationMode">Defines the expiration mode of the cache item.</param>
        /// <param name="timeout">The expiration timeout.</param>
        /// <returns>Provides functionality to evaluate queries against a specific data source.</returns>
        public static IQueryable<TType> Cacheable<TType>(
            this DbSet<TType> query,
            CacheExpirationMode expirationMode,
            TimeSpan timeout) where TType : class
        {
            SanityCheck(query);
            return query.MarkAsNoTracking().TagWith(CachePolicy.Configure(options =>
                options.ExpirationMode(expirationMode).Timeout(timeout)));
        }

        /// <summary>
        /// Returns a new query where the entities returned will be cached.
        /// </summary>
        /// <typeparam name="TType">Entity type.</typeparam>
        /// <param name="query">The input query.</param>
        /// <param name="expirationMode">Defines the expiration mode of the cache item.</param>
        /// <param name="timeout">The expiration timeout.</param>
        /// <param name="saltKey">If you think the computed hash of the query to calculate the cache-key is not enough, set this value.</param>
        /// <returns>Provides functionality to evaluate queries against a specific data source.</returns>
        public static IQueryable<TType> Cacheable<TType>(
            this DbSet<TType> query,
            CacheExpirationMode expirationMode,
            TimeSpan timeout,
            string saltKey) where TType : class
        {
            SanityCheck(query);
            return query.MarkAsNoTracking().TagWith(CachePolicy.Configure(options =>
                options.ExpirationMode(expirationMode).Timeout(timeout).SaltKey(saltKey)));
        }

        /// <summary>
        /// Returns a new query where the entities returned will note be cached.
        /// </summary>
        /// <typeparam name="TType">Entity type.</typeparam>
        /// <param name="query">The input query.</param>
        /// <returns>Provides functionality to evaluate queries against a specific data source.</returns>
        public static IQueryable<TType> NotCacheable<TType>(this IQueryable<TType> query)
        {
            SanityCheck(query);
            return query.TagWith(IsNotCachableMarker);
        }

        /// <summary>
        /// Returns a new query where the entities returned will note be cached.
        /// </summary>
        /// <typeparam name="TType">Entity type.</typeparam>
        /// <param name="query">The input query.</param>
        /// <returns>Provides functionality to evaluate queries against a specific data source.</returns>
        public static IQueryable<TType> NotCacheable<TType>(this DbSet<TType> query) where TType : class
        {
            SanityCheck(query);
            return query.TagWith(IsNotCachableMarker);
        }

        private static void SanityCheck<TType>(IQueryable<TType> query)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (query.Provider is not EntityQueryProvider)
            {
                throw new NotSupportedException("`Cacheable` method is designed only for relational queries.");
            }
        }

        private static IQueryable<TType> MarkAsNoTracking<TType>(this IQueryable<TType> query)
        {
            if (AsNoTrackingMethodInfo == null)
            {
                return query;
            }

            return typeof(TType).GetTypeInfo().IsClass
                ? query.Provider.CreateQuery<TType>(
                    Expression.Call(null, AsNoTrackingMethodInfo.MakeGenericMethod(typeof(TType)), query.Expression))
                : query;
        }
    }
}
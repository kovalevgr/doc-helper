using System;
using System.Data.Common;
using DocHelper.Domain.Cache;
using DocHelper.Infrastructure.Cache.Key;
using DocHelper.Infrastructure.Cache.Policy;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Infrastructure.Cache.Processor
{
    public class CacheProcessor : ICacheProcessor
    {
        private readonly CachePolicyManager _cachePolicyManager;
        private readonly ICacheProvider _cacheProvider;

        public CacheProcessor(CachePolicyManager cachePolicyManager, ICacheProvider cacheProvider)
        {
            _cachePolicyManager = cachePolicyManager;
            _cacheProvider = cacheProvider;
        }

        public T ProcessExecutedCommands<T>(DbCommand command, DbContext context, T result)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var commandText = command.CommandText;
            var cachePolicy = _cachePolicyManager.GetCachePolicy(commandText);
            if (cachePolicy is null)
            {
                return result;
            }

            var cacheKey = CacheKeyProvider.GetKey(command, context, cachePolicy);
            
            // TODO Need implement data serializer
            _cacheProvider.Set(cacheKey.ToString(), result, cachePolicy.CacheTimeout);

            return result;
        }

        public T ProcessExecutingCommands<T>(DbCommand command, DbContext context, T result)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var cachePolicy = _cachePolicyManager.GetCachePolicy(command.CommandText);
            if (cachePolicy is null)
            {
                return result;
            }

            var cacheKey = CacheKeyProvider.GetKey(command, context, cachePolicy);
            
            // TODO Need implement data serializer
            var cacheResult = _cacheProvider.Get<CachedData>(cacheKey.ToString());
            if (cacheResult.IsNull)
            {
                return result;
            }

            return result;
        }
    }
}
using System;
using System.Data.Common;
using System.Globalization;
using DocHelper.Domain.Cache;
using DocHelper.Domain.Cache.Serializable;
using DocHelper.Infrastructure.Cache.Key;
using DocHelper.Infrastructure.Cache.Policy;
using DocHelper.Infrastructure.Cache.Reader;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
            
            if (result is DataReader)
            {
                return result;
            }

            var commandText = command.CommandText;
            var cachePolicy = _cachePolicyManager.GetCachePolicy(commandText);
            if (cachePolicy is null)
            {
                return result;
            }

            var cacheKey = CacheKeyProvider.GetKey(command, context, cachePolicy);

            if (result is DbDataReader dataReader)
            {
                TableRows tableRows;
                using (var dbReaderLoader = new DataReaderLoader(dataReader))
                {
                    tableRows = dbReaderLoader.LoadAndClose();
                }

                _cacheProvider.Set(cacheKey.ToString(), new CachedData {TableRows = tableRows},
                    cachePolicy.CacheTimeout);

                return (T) (object) new DataReader(tableRows);
            }

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

            var cacheResult = _cacheProvider.Get<CachedData>(cacheKey.ToString());
            if (cacheResult.IsNull)
            {
                return result;
            }

            if (result is InterceptionResult<DbDataReader>)
            {
                if (cacheResult.IsNull)
                {
                    using var rows = new DataReader(new TableRows());
                    return (T) Convert.ChangeType(
                        InterceptionResult<DbDataReader>.SuppressWithResult(rows),
                        typeof(T),
                        CultureInfo.InvariantCulture);
                }

                using var dataRows = new DataReader(cacheResult.Value.TableRows);
                return (T) Convert.ChangeType(
                    InterceptionResult<DbDataReader>.SuppressWithResult(dataRows),
                    typeof(T),
                    CultureInfo.InvariantCulture);
            }

            return result;
        }
    }
}
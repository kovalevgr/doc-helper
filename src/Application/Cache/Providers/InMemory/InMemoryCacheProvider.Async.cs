using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocHelper.Domain.Cache;
using DocHelper.Domain.Checker;

namespace DocHelper.Application.Cache.Providers.InMemory
{
    public partial class InMemoryCacheProvider
    {
        public async Task SetAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));
            ArgumentCheck.NotNull(cacheValue, nameof(cacheValue), _options.CacheNulls);
            ArgumentCheck.NotNegativeOrZero(expiration, nameof(expiration));

            if (_options.MaxRdSecond > 0)
            {
                var addSec = new Random().Next(1, _options.MaxRdSecond);
                expiration = expiration.Add(TimeSpan.FromSeconds(addSec));
            }

            var dateTimeOffset = DateTimeOffset.Now.Add(expiration);

            await Task.Run(() => { SetInternal(new CacheEntry(cacheKey, cacheValue, dateTimeOffset)); });
        }

        public async Task<bool> TrySetAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));
            ArgumentCheck.NotNull(cacheValue, nameof(cacheValue), _options.CacheNulls);
            ArgumentCheck.NotNegativeOrZero(expiration, nameof(expiration));

            var dateTimeOffset = DateTimeOffset.Now.Add(expiration);

            return await Task.FromResult(SetInternal(new CacheEntry(cacheKey, cacheValue, dateTimeOffset)));
        }

        public Task SetAllAsync<T>(IDictionary<string, T> value, TimeSpan expiration)
        {
            throw new NotImplementedException();
        }

        public Task<CacheValue<T>> GetAsync<T>(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAllAsync(IEnumerable<string> cacheKeys)
        {
            throw new NotImplementedException();
        }

        public Task<CacheValue<T>> GetAsync<T>(string cacheKey, Func<Task<T>> dataRetriever, TimeSpan expiration)
        {
            throw new NotImplementedException();
        }

        public Task RemoveByPrefixAsync(string prefix)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(string cacheKey, Type type)
        {
            throw new NotImplementedException();
        }
    }
}
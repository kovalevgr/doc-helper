using System;
using System.Collections.Generic;
using System.Linq;
using DocHelper.Application.Cache.Providers.Configuration;
using DocHelper.Domain.Cache;
using DocHelper.Domain.Cache.InMemory;
using DocHelper.Domain.Checker;
using Microsoft.Extensions.Options;

namespace DocHelper.Application.Cache.Providers.InMemory
{
    public partial class InMemoryCacheProvider : ICacheProvider
    {
        private readonly IInMemoryCaching _caching;
        private readonly CacheProviderOptions _options;

        public InMemoryCacheProvider(
            IInMemoryCaching caching,
            IOptions<CacheProviderOptions> options)
        {
            _caching = caching;
            _options = options.Value;
        }

        public void Set<T>(string cacheKey, T cacheValue, TimeSpan expiration)
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

            _caching.Set(new CacheEntry(cacheKey, cacheValue, dateTimeOffset));
        }

        public bool TrySet<T>(string cacheKey, T cacheValue, TimeSpan expiration)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));
            ArgumentCheck.NotNull(cacheValue, nameof(cacheValue), _options.CacheNulls);
            ArgumentCheck.NotNegativeOrZero(expiration, nameof(expiration));

            var dateTimeOffset = DateTimeOffset.Now.Add(expiration);

            return _caching.Set(new CacheEntry(cacheKey, cacheValue, dateTimeOffset), true);
        }

        public void SetAll<T>(IDictionary<string, T> values, TimeSpan expiration)
        {
            ArgumentCheck.NotNegativeOrZero(expiration, nameof(expiration));
            ArgumentCheck.NotNullAndCountGTZero(values, nameof(values));

            var dateTimeOffset = DateTimeOffset.Now.Add(expiration);

            foreach (var entry in values) _caching.Set(new CacheEntry(entry.Key, entry.Value, dateTimeOffset));
        }

        public CacheValue<T> Get<T>(string cacheKey)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            var result = _caching.Get<T>(cacheKey);
            if (result.HasValue)
            {
                return result;
            }

            return CacheValue<T>.NoValue;
        }

        public void Remove(string cacheKey)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            _caching.Remove(cacheKey);
        }

        public void RemoveAll(IEnumerable<string> cacheKeys)
        {
            var keys = cacheKeys.ToList();
            ArgumentCheck.NotNullAndCountGTZero(keys, nameof(cacheKeys));

            foreach (var key in keys) _caching.Remove(key);
        }

        public void RemoveByPrefix(string prefix)
        {
            var keys = _caching.Keys(prefix);

            foreach (var key in keys) _caching.Remove(key);
        }

        public bool Exists(string cacheKey)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            return _caching.Exists(cacheKey);
        }
    }
}
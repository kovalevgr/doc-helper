using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DocHelper.Application.Cache.Providers.Configuration;
using DocHelper.Domain.Cache;
using DocHelper.Domain.Checker;
using Microsoft.Extensions.Options;

namespace DocHelper.Application.Cache.Providers.InMemory
{
    public partial class InMemoryCacheProvider : ICacheProvider
    {
        private readonly CacheProviderOptions _options;

        public InMemoryCacheProvider(IOptions<CacheProviderOptions> options)
        {
            _memory = new ConcurrentDictionary<string, CacheEntry>();
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

            SetInternal(new CacheEntry(cacheKey, cacheValue, dateTimeOffset));
        }

        public bool TrySet<T>(string cacheKey, T cacheValue, TimeSpan expiration)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));
            ArgumentCheck.NotNull(cacheValue, nameof(cacheValue), _options.CacheNulls);
            ArgumentCheck.NotNegativeOrZero(expiration, nameof(expiration));

            var dateTimeOffset = DateTimeOffset.Now.Add(expiration);

            return SetInternal(new CacheEntry(cacheKey, cacheValue, dateTimeOffset), true);
        }

        public void SetAll<T>(IDictionary<string, T> values, TimeSpan expiration)
        {
            ArgumentCheck.NotNegativeOrZero(expiration, nameof(expiration));
            ArgumentCheck.NotNullAndCountGTZero(values, nameof(values));

            var dateTimeOffset = DateTimeOffset.Now.Add(expiration);

            foreach (var entry in values) SetInternal(new CacheEntry(entry.Key, entry.Value, dateTimeOffset));
        }

        public CacheValue<T> Get<T>(string cacheKey)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            var result = BaseGet<T>(cacheKey);
            if (result.HasValue)
            {
                return result;
            }

            return CacheValue<T>.NoValue;
        }

        public void Remove(string cacheKey)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            BaseRemove(cacheKey);
        }

        public void RemoveAll(IEnumerable<string> cacheKeys)
        {
            var keys = cacheKeys.ToList();
            ArgumentCheck.NotNullAndCountGTZero(keys, nameof(cacheKeys));

            foreach (var key in keys) BaseRemove(key);
        }

        public void RemoveByPrefix(string prefix)
        {
            var keys = _memory.Keys.Where(x => x.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var key in keys) BaseRemove(key);
        }

        public bool Exists(string cacheKey)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            return _memory.TryGetValue(cacheKey, out _);
        }
    }
}
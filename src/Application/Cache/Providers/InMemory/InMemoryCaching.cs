using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DocHelper.Domain.Cache;
using DocHelper.Domain.Cache.InMemory;

namespace DocHelper.Application.Cache.Providers.InMemory
{
    public class InMemoryCaching : IInMemoryCaching
    {
        private static ConcurrentDictionary<string, CacheEntry> _memory;

        public InMemoryCaching()
        {
            if (_memory is null)
            {
                _memory = new ConcurrentDictionary<string, CacheEntry>();
            }
        }
        
        public bool Set(CacheEntry entry, bool addOnly = false)
        {
            if (addOnly)
            {
                if (!_memory.TryAdd(entry.Key, entry))
                {
                    if (!_memory.TryGetValue(entry.Key, out _) || entry.ExpiresAt >= DateTimeOffset.UtcNow)
                        return false;

                    _memory.AddOrUpdate(entry.Key, entry, (k, cacheEntry) => entry);
                }
            }
            else
            {
                _memory.AddOrUpdate(entry.Key, entry, (k, cacheEntry) => entry);
            }

            return true;
        }

        public CacheValue<T> Get<T>(string key)
        {
            if (!_memory.TryGetValue(key, out var cacheEntry))
            {
                return CacheValue<T>.NoValue;
            }

            try
            {
                var value = cacheEntry.GetValue<T>(true);
                return new CacheValue<T>(value, true);
            }
            catch (Exception)
            {
                return CacheValue<T>.NoValue;
            }
        }

        public void Remove(string key)
        {
            _memory.TryRemove(key, out _);
        }

        public List<string> Keys(string prefix)
        {
            return _memory.Keys.Where(x => x.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public bool Exists(string cacheKey)
        {
            return _memory.TryGetValue(cacheKey, out _);
        }
    }
}
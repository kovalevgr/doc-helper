using System;
using System.Collections.Concurrent;
using DocHelper.Domain.Cache;
using DocHelper.Domain.Helpers;

namespace DocHelper.Application.Cache.Providers.InMemory
{
    public partial class InMemoryCacheProvider
    {
        private readonly ConcurrentDictionary<string, CacheEntry> _memory;

        private bool SetInternal(CacheEntry entry, bool addOnly = false)
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

        public CacheValue<T> BaseGet<T>(string key)
        {
            if (!_memory.TryGetValue(key, out var cacheEntry))
            {
                return CacheValue<T>.NoValue;
            }

            try
            {
                var value = cacheEntry.GetValue<T>();
                return new CacheValue<T>(value, true);
            }
            catch (Exception)
            {
                return CacheValue<T>.NoValue;
            }
        }

        public void BaseRemove(string key)
        {
            _memory.TryRemove(key, out _);
        }

        public class CacheEntry
        {
            public CacheEntry(string key, object value, DateTimeOffset expiresAt)
            {
                Key = key;
                Value = value;
                ExpiresAt = expiresAt;
            }

            internal string Key { get; private set; }
            private object Value { get; set; }
            internal DateTimeOffset ExpiresAt { get; set; }

            public T GetValue<T>()
            {
                object val = Value;

                var t = typeof(T);

                if (t == TypeHelper.BoolType || t == TypeHelper.StringType || t == TypeHelper.CharType ||
                    t == TypeHelper.DateTimeType)
                    return (T) Convert.ChangeType(val, t);

                if (t == TypeHelper.NullableBoolType || t == TypeHelper.NullableCharType ||
                    t == TypeHelper.NullableDateTimeType)
                    return val == null ? default(T) : (T) Convert.ChangeType(val, Nullable.GetUnderlyingType(t));

                return (T) val;
            }
        }
    }
}
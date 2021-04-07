using System.Collections.Generic;

namespace DocHelper.Domain.Cache.InMemory
{
    public interface IInMemoryCaching
    {
        bool Set(CacheEntry entry, bool addOnly = false);

        CacheValue<T> Get<T>(string key);

        void Remove(string key);

        List<string> Keys(string prefix);

        bool Exists(string cacheKey);
    }
}
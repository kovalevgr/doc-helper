using System;
using System.Collections.Generic;

namespace DocHelper.Domain.Cache.Key
{
    public class CacheKey
    {
        /// <summary>
        /// The computed key of the input LINQ query.
        /// </summary>
        public string Key { set; get; } = default!;

        /// <summary>
        /// Hash of the input LINQ query's computed key.
        /// </summary>
        public string KeyHash { get; set; } = default!;
        
        /// <summary>
        /// Determines which entities are used in this LINQ query.
        /// This array will be used to invalidate the related cache of all related queries automatically.
        /// </summary>
        public ISet<string> CacheDependencies { get; set; }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        public override bool Equals(object obj)
        {
            if (!(obj is CacheKey efCacheKey))
                return false;

            return string.Equals(this.KeyHash, efCacheKey.KeyHash, StringComparison.Ordinal);
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                const int hash = 17;
                return (hash * 23) + KeyHash.GetHashCode(StringComparison.Ordinal);
            }
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString()
        {
            return $"KeyHash: {KeyHash}, CacheDependencies: {string.Join(", ", CacheDependencies)}.";
        }
    }
}
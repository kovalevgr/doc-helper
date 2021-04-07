using System;

namespace DocHelper.Domain.Cache.Policy
{
    public class CachePolicy
    {
        public const char ItemsSeparator = '|';

        public const string PartsSeparator = "-->";

        public static readonly string Prefix = $"-- {nameof(CachePolicy)}";

        /// <summary>
        /// Defines the expiration mode of the cache item.
        /// Its default value is Absolute.
        /// </summary>
        public CacheExpirationMode CacheExpirationMode { get; set; }

        /// <summary>
        /// The expiration timeout.
        /// Its default value is 20 minutes later.
        /// </summary>
        public TimeSpan CacheTimeout { get; set; } = TimeSpan.FromMinutes(20);

        /// <summary>
        /// If you think the computed hash of the query to calculate the cache-key is not enough, set this value.
        /// Its default value is string.Empty.
        /// </summary>
        public string CacheSaltKey { get; private set; } = string.Empty;

        /// <summary>
        /// Determines the default Cacheable method
        /// </summary>
        private bool IsDefaultCacheableMethod { set; get; }

        /// <summary>
        /// Defines the expiration mode of the cache item.
        /// Its default value is Absolute.
        /// </summary>
        public CachePolicy ExpirationMode(CacheExpirationMode expirationMode)
        {
            CacheExpirationMode = expirationMode;
            return this;
        }

        /// <summary>
        /// The expiration timeout.
        /// Its default value is 20 minutes later.
        /// </summary>
        public CachePolicy Timeout(TimeSpan timeout)
        {
            CacheTimeout = timeout;
            return this;
        }

        /// <summary>
        /// If you think the computed hash of the query to calculate the cache-key is not enough, set this value.
        /// Its default value is string.Empty.
        /// </summary>
        public CachePolicy SaltKey(string saltKey)
        {
            CacheSaltKey = saltKey;
            return this;
        }

        /// <summary>
        /// Determines the default Cacheable method
        /// </summary>
        public CachePolicy DefaultCacheableMethod(bool state)
        {
            IsDefaultCacheableMethod = state;
            return this;
        }

        /// <summary>
        /// Determines the Expiration time of the cache.
        /// </summary>
        public static string Configure(Action<CachePolicy> options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var cachePolicy = new CachePolicy();
            options.Invoke(cachePolicy);
            return cachePolicy.ToString();
        }

        /// <summary>
        /// Represents the textual form of the current object
        /// </summary>
        public override string ToString()
        {
            return
                $"{nameof(CachePolicy)} {PartsSeparator} {CacheExpirationMode}{ItemsSeparator}{CacheTimeout}{ItemsSeparator}{CacheSaltKey}{ItemsSeparator}{ItemsSeparator}{IsDefaultCacheableMethod}"
                    .TrimEnd(ItemsSeparator);
        }
    }
}
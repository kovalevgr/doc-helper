namespace DocHelper.Domain.Cache.Policy
{
    public enum CacheExpirationMode
    {
        /// <summary>
        /// Defines absolute expiration. The item will expire after the expiration timeout.
        /// </summary>
        Absolute,

        /// <summary>
        /// Defines sliding expiration. The expiration timeout will be refreshed on every access.
        /// </summary>
        Sliding
    }
}
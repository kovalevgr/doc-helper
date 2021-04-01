using System;
using DocHelper.Domain.Cache.Configuration;

namespace DocHelper.Infrastructure.Cache.Configuration
{
    public class CacheOptions
    {
        public bool UseCache { get; set; } = false;
        
        public CacheProviders Provider { get; set; }

        public TimeSpan Timeout { get; set; }
    }
}
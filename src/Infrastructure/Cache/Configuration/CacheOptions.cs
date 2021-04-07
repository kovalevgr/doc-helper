using System;
using DocHelper.Domain.Cache.Configuration;

namespace DocHelper.Infrastructure.Cache.Configuration
{
    public class CacheOptions
    {
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(30);
    }
}
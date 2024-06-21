using Microsoft.Extensions.Caching.Memory;
using System;

namespace CacheService
{
    public class Manager
    {
        private static IMemoryCache? _cache;
        private static object _lock = new ();

        public static IMemoryCache GetCacheInstance()
        {
            lock(_lock)
            {
                // if the cache is not initialized (_cache == null), create a new instance
                _cache ??= new MemoryCache(new MemoryCacheOptions());
            }
            
            return _cache;
        }
    }
}

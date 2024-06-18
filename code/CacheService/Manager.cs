using Microsoft.Extensions.Caching.Memory;
using System;

namespace CacheService
{
    public class Manager(IMemoryCache cache) : IDisposable
    {
        private readonly IMemoryCache _cache = cache;

        public void Set<T>(string key, T value)
        {
            _cache.Set(key, value);
        }

        public void Set<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow)
        {
            _cache.Set(key, value, absoluteExpirationRelativeToNow);
        }

        public T? Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Dispose()
        {
            _cache.Dispose();

            //Message CA1816  Change Manager.Dispose() to call GC.SuppressFinalize(object).This will prevent derived types that introduce a finalizer from needing to re - implement 'IDisposable' to call it. A method that is an implementation of Dispose does not call GC.SuppressFinalize; or a method that is not an implementation of Dispose calls GC.SuppressFinalize; or a method calls GC.SuppressFinalize and passes something other than this.
            GC.SuppressFinalize(this); 
        }
    }
}

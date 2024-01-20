using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Temp.DAL.Interfaces;

namespace Temp.DAL.Cache
{
    public class InMemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRedisCacheManager _redisCache;
        private readonly object _syncObj = false;
        private readonly bool _enableRedisCache = false;
        private readonly bool _enableCache = false;
        public InMemoryCacheManager(IServiceProvider serviceProvider)
        {
            _memoryCache = serviceProvider.GetService<IMemoryCache>();
            _redisCache = serviceProvider.GetService<IRedisCacheManager>();
            _enableRedisCache = _redisCache != null;
            _enableCache = _memoryCache != null;
        }

        public async Task<T> GetOrAddValue<T>(string key, Func<Task<T>> factoryCallback, TimeSpan? absoluteExpirationRelativeToNow = null, bool enableSliding = false)
        {
            if (!_enableCache) return await factoryCallback();

            var result = _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
                entry.AbsoluteExpiration = absoluteExpirationRelativeToNow == null ? null : DateTimeOffset.Now.Add(absoluteExpirationRelativeToNow.Value);
                entry.SlidingExpiration = enableSliding ? TimeSpan.FromHours(1) : null;
                if (_enableRedisCache)
                {
                    return await _redisCache.GetOrAddValue(key, factoryCallback, absoluteExpirationRelativeToNow, enableSliding);
                }

                var cacheEntry = await factoryCallback();
                return cacheEntry;

            });
            return await result;
        }

        public T Get<T>(string key)
        {
            if (!_enableCache) return (T)default;
            var result = _memoryCache.Get<T>(key);
            if (result == null && _enableRedisCache)
            {
                return _redisCache.Get<T>(key);
            }
            return result;
        }


        public void Invalidate(string key, bool internalCache)
        {
            if (!_enableCache) return;

            if (_enableRedisCache && !internalCache)
            {
                _redisCache.Invalidate(key);
            }
            _memoryCache.Remove(key);
        }
    }
}

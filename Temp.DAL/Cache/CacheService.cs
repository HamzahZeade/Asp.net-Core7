using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using Temp.DAL.Interfaces;

namespace Temp.DAL.Cache
{
    internal class CacheService : ICacheService
    {
        private static readonly ConcurrentDictionary<string, bool> CacheKeys = new ConcurrentDictionary<string, bool>();

        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            string? cacheValue = await _distributedCache.GetStringAsync(key);

            if (cacheValue is null)
            {
                return null;
            }

            T? result = JsonConvert.DeserializeObject<T>(cacheValue);

            return result;
        }
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory) where T : class
        {
            T? cacheValue = await GetAsync<T>(key);
            if (cacheValue is not null)
            {
                return cacheValue;

            }
            cacheValue = await factory();
            await SetAsync(key, cacheValue);
            return cacheValue;

        }
        public async Task SetAsync<T>(string key, T value) where T : class
        {
            string? cacheValue = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, cacheValue);
            CacheKeys.TryAdd(key, false);
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
            CacheKeys.TryRemove(key, out _);
        }

        public async Task RemoveAsyncPrefix(string prefixKey)
        {
            IEnumerable<Task> tasks = CacheKeys.Keys
                .Where(k => k.StartsWith(prefixKey))
                .Select(k => RemoveAsync(k));

            await Task.WhenAll(tasks);
        }


    }

}

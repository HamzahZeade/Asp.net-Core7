using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Text.Json;
using Temp.Models.Dtos;

namespace Temp.DAL.Interfaces
{
    public interface IRedisCacheManager
    {
        public Task<T> GetOrAddValue<T>(string key, Func<Task<T>> factoryCallback, TimeSpan? absoluteExpirationRelativeToNow = null, bool enableSliding = false);

        public T Get<T>(string key);

        public void Invalidate(string key);

    }
    public class RedisCacheManager : IRedisCacheManager
    {
        private readonly StackExchange.Redis.IDatabase _database;
        private readonly ICachePublisher _publisher;

        public RedisCacheManager(IServiceProvider serviceProvider)
        {
            var connection = serviceProvider.GetRequiredService<IConnectionMultiplexer>();
            _database = connection.GetDatabase();
            _publisher = serviceProvider.GetRequiredService<ICachePublisher>();
        }

        public async Task<T> GetOrAddValue<T>(string key, Func<Task<T>> factoryCallback, TimeSpan? absoluteExpirationRelativeToNow = null,
            bool enableSliding = false)
        {
            var packedBytes = _database.StringGet(key);
            if (!packedBytes.IsNull)
            {
                if (packedBytes.IsNullOrEmpty)
                {
                    return (T)default;
                }
                var cacheItem = JsonSerializer.Deserialize<CachedDto<T>>(packedBytes.ToString());
                return cacheItem != null ? cacheItem.Data : (T)default;
            }
            var cacheEntry = await factoryCallback();
            var item = JsonSerializer.Serialize(new CachedDto<T>(cacheEntry));
            _database.StringSet(
                key,
                item,
                absoluteExpirationRelativeToNow,
                When.Always,
                CommandFlags.FireAndForget);
            return cacheEntry;
        }

        public T Get<T>(string key)
        {
            var packedBytes = _database.StringGet(key);
            if (packedBytes.IsNull) return (T)default;

            var cacheItem = JsonSerializer.Deserialize<CachedDto<T>>(packedBytes);
            return cacheItem != null ? cacheItem.Data : (T)default;
        }

        public void Invalidate(string key)
        {
            try
            {
                _database.KeyDelete(key);
            }
            catch (Exception e)
            {
                throw;
            }
            _publisher.NotifyDelete(key);
        }
    }
}

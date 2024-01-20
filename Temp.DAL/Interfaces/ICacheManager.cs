namespace Temp.DAL.Interfaces
{
    public interface ICacheManager
    {
        public Task<T> GetOrAddValue<T>(string key, Func<Task<T>> factoryCallback, TimeSpan? absoluteExpirationRelativeToNow = null, bool enableSliding = false);

        public T Get<T>(string key);


        public void Invalidate(string key, bool internalCache = false);
    }
}

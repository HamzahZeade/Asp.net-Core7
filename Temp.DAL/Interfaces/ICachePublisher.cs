namespace Temp.DAL.Interfaces
{
    public interface ICachePublisher
    {
        //void NotifyUpdate(string key );
        //void NotifyUpdate(string key,  TimeSpan? specificTimeToLive);
        void NotifyDelete(string key);
    }
}

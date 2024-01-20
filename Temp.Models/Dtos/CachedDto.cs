namespace Temp.Models.Dtos
{
    public class CachedDto<T>
    {
        public CachedDto(T data)
        {
            Data = data;
            CachedDateTime = DateTime.Now;
        }
        public T Data { get; set; }
        public DateTime CachedDateTime { get; private set; }
    }
}

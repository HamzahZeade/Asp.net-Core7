namespace Temp.Models.Base
{
    public class PageList<T>
    {
        public IEnumerable<T> Page { get; set; }
        public int PageCount => Page?.Count() ?? 0;
        public int TotalRow { get; set; }
    }
}

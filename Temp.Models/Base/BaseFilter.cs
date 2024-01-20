namespace Temp.Models.Base
{
    public class BaseFilter
    {
        public void Apply(Func<bool> predicate, Action action)
        {
            if (predicate())
            {
                action();
            }
        }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 30;

        public string OrderBy { get; set; }

        public bool IsSortDesc { get; set; }

        public string SortOrder => IsSortDesc ? "desc" : "asc";
    }
    public class BaseFilter<T> : BaseFilter
    {
        public T Filters { get; set; }
    }
}

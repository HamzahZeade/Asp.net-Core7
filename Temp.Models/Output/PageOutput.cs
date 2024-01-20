namespace Temp.Models.Output
{
    public class PageOutput<T>
    {
        #region ////////// Fields //////////
        public T Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordsFiltered { get; set; }
        public int TotalRecords { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        #endregion ////////// Fields //////////

        #region ////////// Constructors //////////
        public PageOutput()
        {
            IsSuccess = true;
        }

        public PageOutput(List<string> errors)
        {
            Errors = errors;
            IsSuccess = false;
        }

        public PageOutput(T data, int startIndex, int pageLength, int totalRecords)
        {
            Data = data;
            PageIndex = startIndex;
            PageSize = pageLength;
            TotalRecords = totalRecords;
            IsSuccess = true;
        }

        #endregion ////////// Constructors //////////
    }
}

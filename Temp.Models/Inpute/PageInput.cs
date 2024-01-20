namespace Temp.Models.Inpute
{
    public class PageInput<T>
    {
        #region Properties
        public T Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        #endregion Properties

        #region Constructors
        public PageInput()
        {
            Data = default;
        }
        #endregion Constructors

    }


    public class PagerInput
    {
        #region Properties
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        #endregion Properties

        #region Constructors
        public PagerInput()
        {

        }

        public PagerInput(int startIndex, int pageLength)
        {
            PageIndex = startIndex;
            PageSize = pageLength;
        }
        #endregion Constructors

    }
}

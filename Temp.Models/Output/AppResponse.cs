namespace Temp.Models.Output
{
    public class AppResponse<T> : AppResponse
    {
        #region Properties
        public T Data { get; set; }
        #endregion Properties

        #region Constructors
        //public AppResponse(List<Entities.AppEntities.GalleryVideo> videoDtos)
        //{
        //    Data = default;
        //    IsSuccess = true;
        //    List<string> Errors = new();
        //}

        public AppResponse(T data)
        {
            Data = data;
            IsSuccess = true;
        }
        #endregion Constructors

        #region Methods
        #endregion Methods
    }

    public class AppResponse
    {
        #region Properties
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        #endregion Properties

        #region Constructors
        public AppResponse()
        {
            IsSuccess = true;
            Errors = new();
        }

        public AppResponse(params string[] errors)
        {
            Errors = errors.ToList();
            IsSuccess = false;
        }
        #endregion Constructors

        #region Methods
        public void AddError(string error)
        {

            Errors.Add(error);

            if (IsSuccess)
                IsSuccess = false;
        }

        public void AddErrors(List<string> errors)
        {

            Errors.AddRange(errors);

            if (IsSuccess)
                IsSuccess = false;
        }
        #endregion Methods
    }

}

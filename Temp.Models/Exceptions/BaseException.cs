namespace Temp.Models.Exceptions
{
    public abstract class BaseException : Exception
    {
        public string Code { get; private set; }
        public bool IsToasterMessage { get; set; }
        protected BaseException(string code, string message, Exception innerException = null) : base(message, innerException)
        {
            this.Code = code;
        }

        public override string ToString()
        {
            return $"{Code}: {Message}, {base.ToString()}";
        }
    }
}

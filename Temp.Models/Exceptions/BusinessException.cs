using Temp.Models.Constants;

namespace Temp.Models.Exceptions
{
    public class BusinessException : BaseException
    {
        public string LabelId { get; private set; }
        public BusinessException(Enum errorEnum) : base(((int)(object)errorEnum).ToString(), errorEnum.GetEnumDisplayText())
        {
            this.LabelId = errorEnum.GetLabelId();
            this.IsToasterMessage = string.IsNullOrEmpty(LabelId);
        }
        public BusinessException(System.Enum errorEnum, params string[] arg) : base(((int)(object)errorEnum).ToString(), string.Format(errorEnum.GetEnumDisplayText(), arg))
        {
            this.LabelId = errorEnum.GetLabelId();
            this.IsToasterMessage = string.IsNullOrEmpty(LabelId);
        }

        public BusinessException(string code, string message) : base(code, message)
        {
            this.IsToasterMessage = true;
        }

        public BusinessException(string message) : base(ErrorConstants.Codes.InvalidData, message)
        {
            this.IsToasterMessage = true;
        }

    }
}

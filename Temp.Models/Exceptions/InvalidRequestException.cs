using Temp.Models.Constants;

namespace Temp.Models.Exceptions
{
    public class InvalidRequestException : BaseException
    {
        public InvalidRequestException(string message) : base(ErrorConstants.Codes.InvalidRequest, message)
        {
        }

        public InvalidRequestException() : base(ErrorConstants.Codes.InvalidRequest, ErrorConstants.Messages.InvalidRequest)
        {

        }
    }
}

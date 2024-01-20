namespace Temp.Models.Constants
{
    internal class ErrorConstants
    {
        internal class Codes
        {
            internal static string UnauthorizedAccess => "401";
            internal static string InvalidRequest => "7001";
            internal static string InternalSystemError => "5001";
            internal static string InvalidData => "7002";
        }

        internal class Messages
        {
            internal static string UnauthorizedAccess => "Unauthorized Request";
            internal static string InvalidRequest => "Invalid request, one or more of required data not exists or is invalid";
            internal static string InternalSystemError => "Internal system error";
        }

    }
}

using System.Security.Claims;

namespace Temp.Models.InterFaces
{
    public interface ITokenHelper
    {
        public (string tokenString, DateTime tokenExpireDate) GenerateAccessToken(IEnumerable<Claim> claims);
        public string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

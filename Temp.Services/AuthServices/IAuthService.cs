using Temp.Models.Inpute;
using Temp.Models.Output;

namespace Temp.Services.AuthServices
{
    public interface IAuthService
    {
        Task<AuthenticatedResponse> LoginUser(LoginInpute user);
    }
}

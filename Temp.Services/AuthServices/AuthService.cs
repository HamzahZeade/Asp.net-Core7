using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tem.Base.Common;
using Temp.Models.Entities.GenericEntities;
using Temp.Models.Entities.Models;
using Temp.Models.Errors;
using Temp.Models.Exceptions;
using Temp.Models.Inpute;
using Temp.Models.InterFaces;
using Temp.Models.Output;

namespace Temp.Services.AuthServices
{
    public class AuthService : BaseService<User>, IAuthService
    {
        private readonly IOptions<JwtAuthenticationOptions> _jwtOptions;
        private readonly ITokenHelper _tokenHelper;

        public AuthService(IServiceProvider serviceProvider, IOptions<JwtAuthenticationOptions> jwtOptions) : base(serviceProvider)
        {
            _tokenHelper = GetService<ITokenHelper>();
            _jwtOptions = jwtOptions;
        }
        public async Task<AuthenticatedResponse> LoginUser(LoginInpute loginInpute)
        {
            if (loginInpute is null)
            {
                throw new InvalidRequestException("Invalid request");
            }
            if (loginInpute.UserName.IsNullOrEmpty())
            {
                throw new BusinessException(ErrorKeys.UsernameFieldIsRequired);
            }
            //var user = await Repository.Get(x => x.UserName == loginInpute.UserName && x.IsActive && x.IsDeleted == false).Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync();


            //if (user is null)
            //{
            //    throw new BusinessException(ErrorKeys.InvalidUsernameOrPassword);
            //}

            //var cacheService = GetService<ICacheManager>();
            //var rolePermissions = await cacheService.GetOrAddValue(CacheKeys.RolePermission, async () =>
            //{
            //    var rolePermissionRepository = GetService<IGenericRepo<RolePermission>>();
            //    return await rolePermissionRepository.Get().ProjectToType<RolePermissionDto>().ToListAsync();
            //});

            //var user = await Repository.Get(x => x.Username == loginInpute.UserName && x.IsActive && x.IsDeleted == false).Include(x => x.Orgnization).Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync();

            // Replace this with actual user authentication logic (e.g., database lookup)
            if (loginInpute.UserName == "johndoe" && loginInpute.Password == "def@123")
            {
                var tokenString = GenerateJwtToken();
                return new AuthenticatedResponse { Token = tokenString };
            }

            return null; // Authentication failed
        }

        private string GenerateJwtToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.IssuerSigningKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _jwtOptions.Value.ValidIssuer,
                audience: _jwtOptions.Value.ValidAudience,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
    }

}

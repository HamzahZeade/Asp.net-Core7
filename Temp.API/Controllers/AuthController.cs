using Microsoft.AspNetCore.Mvc;
using Temp.Models.Inpute;
using Temp.Services.AuthServices;

namespace Temp.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInpute user)
        {
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }

            var authenticatedResponse = await _authService.LoginUser(user);

            if (authenticatedResponse != null)
            {
                return Ok(authenticatedResponse);
            }

            return Unauthorized();
        }

    }

}

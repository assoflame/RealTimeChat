using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared.DataTransferObjects;

namespace Chat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto userSignUpDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _serviceManager.AuthService.SignUpAsync(userSignUpDto);

            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto userSignInDto)
        {
            if (!await _serviceManager.AuthService.ValidateUserAsync(userSignInDto))
                return Unauthorized();

            return Ok(new { Token = _serviceManager.AuthService.CreateToken() });
        }
    }
}

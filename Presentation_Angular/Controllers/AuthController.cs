using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Angular.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet("user")]
        public async Task<ActionResult<ChangeUserDetailsDto>> GetCurrentUser()
        {
            return Ok(await _identityService.GetCurrentUserDetails());
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<object>> Login(LoginDto loginDto)
        {
            var token = await _identityService.Login(loginDto);

            return Ok(new
            {
                token
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<object>> Register(RegisterDto registerDto)
        {
            var token = await _identityService.Register(registerDto);

            return Ok(new
            {
                token
            });
        }

        [HttpPut("password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            await _identityService.ChangePassword(changePasswordDto);

            return NoContent();
        }

        [HttpPut("user")]
        public async Task<ActionResult<object>> ChangeUserDetails(ChangeUserDetailsDto changeUserDetailsDto)
        {
            // Sends new token with updated username
            var token = await _identityService.ChangeUserDetails(changeUserDetailsDto);

            return Ok(new
            {
                token
            });
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteUser(DeleteUserDto deleteUserDto)
        {
            await _identityService.DeleteUser(deleteUserDto);

            return NoContent();
        }
    }
}
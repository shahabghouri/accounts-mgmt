using Accounts.Common.DTO;
using Accounts.Services.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Accounts.API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserDTO request)
        {
            await _userService.Signup(request);
            return Ok();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestDTO request)
        {
            var response = await _userService.Authenticate(request);

            if (response == null)
            {
                return Unauthorized(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }
        [HttpGet("auth/balance")]
        [Authorize]
        public async Task<IActionResult> GetBalance()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var balance = await _userService.GetBalanceAsync(userId);
            return Ok(new { balance });
        }
    }
}

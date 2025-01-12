using Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Services;
namespace Portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(
            IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (result.Success)
            {
                return Ok(new { Message = result.Message });
            }

            return BadRequest(new { Error = result.Message });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (result.Success)
            {
                return Ok( new { Message = result.Message });
            }

            return Unauthorized(new { Error = result.Message });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok(new { Message = "User logged out successfully" });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var email = _authService.GetUserEmail(User);
            return Ok(new { Email = email });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(string id)
        {
            var userDTO = await _authService.GetUserDTOById(id);
            if (userDTO == null)
            {
                return NotFound();
            }
            return Ok(userDTO);
        }

    }
}

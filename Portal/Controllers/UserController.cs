    using Portal.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using System.Security.Claims;
    using System.IdentityModel.Tokens.Jwt;
    namespace Portal.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class UserController : ControllerBase
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly ILogger<UserController> _logger;

            public UserController(
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                ILogger<UserController> logger)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _logger = logger;
            }

            [HttpPost("register")]
            [AllowAnonymous]
            public async Task<IActionResult> Register([FromBody] RegisterModel model)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    return Ok(new { Message = "User registered successfully" });
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return BadRequest(ModelState);
            }

            [HttpPost("login")]
            [AllowAnonymous]
            public async Task<IActionResult> Login([FromBody] LoginModel model)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Ok(new { Message = "User logged in successfully"});
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return BadRequest(new { Error = "User account locked out" });
                }

                return Unauthorized(new { Error = "Invalid login attempt" });
            }

            [HttpPost("logout")]
            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
                return Ok(new { Message = "User logged out successfully" });
            }

            [HttpGet("me")]
            [Authorize]
            public IActionResult Me()
            {
                var email = User.Identity?.Name;
                return Ok(new { Email = email });
            }

        }

        public class RegisterModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }
    }

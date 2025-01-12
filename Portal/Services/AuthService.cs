using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NuGet.Protocol;
using Portal.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public string GetUserEmail(ClaimsPrincipal user)
        {
            return user.Identity?.Name;
        }

        public async Task<(bool Success, string Message)> LoginAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return (true, "User logged in successfully.");
            }

            if (result.IsLockedOut) 
            {
                _logger.LogWarning("User accound locked out.");
                return (false, "User account locked out.");
            }
            return (false, "Invalid login attempt");
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterModel model)
        {
            var user = new User
            {
                EmailConfirmed = true,
                Email = model.Email,
                UserName = model.Username,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                return (true, "User registered successfully");
            }
            return (false, "User registration failed.");
        }
    }
}

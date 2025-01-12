using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NuGet.Protocol;
using Portal.Models;
using Portal.Models.DTO;
using Portal.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthService> _logger;
        private readonly IRepositoryWrapper _unitOfWork;

        public AuthService(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            ILogger<AuthService> logger,
            IRepositoryWrapper unitOfWork
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDTO> GetUserDTOById(string id)
        {
            var User = await _unitOfWork.Users.GetUserByIdAsync(id);
            var userDTO = new UserDTO();
            userDTO.SetFromUser(User);
            return userDTO;
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

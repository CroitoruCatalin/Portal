using Portal.Models;
using System.Security.Claims;

namespace Portal.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterModel model);
        Task<(bool Success, string Message)> LoginAsync(LoginModel model);
        Task LogoutAsync();
        string GetUserEmail(ClaimsPrincipal user);
    }
}

using Portal.Models;
using Portal.Models.DTO;
using System.Security.Claims;

namespace Portal.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterModel model);
        Task<(bool Success, string Message)> LoginAsync(LoginModel model);
        Task LogoutAsync();
        string GetUserEmail(ClaimsPrincipal user);
        Task<UserDTO> GetUserDTOById(string id);
    }
}

using Portal.Models;
namespace Portal.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string userId);
    }
}

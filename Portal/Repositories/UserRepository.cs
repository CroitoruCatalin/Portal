using Microsoft.EntityFrameworkCore;
using Portal.Models;
using Microsoft.Extensions.Logging;

namespace Portal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PortalContext _context;

        public UserRepository(PortalContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
               
            }
            return user;
        }
    }
}

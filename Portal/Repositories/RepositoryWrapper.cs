using Portal.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Portal.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly PortalContext _context;
        private IPostRepository _postRepository;
        private IUserRepository _userRepository;

        public RepositoryWrapper(PortalContext context)
        {
            _context = context;
        }

        public IPostRepository Posts
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new PostRepository(_context);
                    if (_postRepository == null) throw new ArgumentNullException("_postRepository");
                }
                return _postRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

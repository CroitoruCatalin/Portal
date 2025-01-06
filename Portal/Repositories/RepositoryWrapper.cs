using Portal.Models;

namespace Portal.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly PortalContext _context;

        public IPostRepository Posts { get; }

        public RepositoryWrapper(PortalContext context, IPostRepository postRepository)
        {
            _context = context;
            Posts = postRepository;
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

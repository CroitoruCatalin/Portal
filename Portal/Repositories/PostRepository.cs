using Microsoft.EntityFrameworkCore;
using Portal.Models;

namespace Portal.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PortalContext _context;

        public PostRepository(PortalContext context)
        {
            if(context == null) throw new ArgumentNullException("context");
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(p => p.Author).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await _context.Posts.Include(p => p.Author)
                                       .FirstOrDefaultAsync(p => p.PostID == postId);
        }

        public async Task CreatePostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
        }

        public async Task UpdatePostAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}

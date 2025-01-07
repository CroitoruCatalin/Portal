using Portal.Models;
using Portal.Models.DTO;
using System.Security.Claims;

namespace Portal.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int postId);
        Task<Post> CreatePostAsync(PostDTO dto, ClaimsPrincipal user);
        Task UpdatePostAsync(Post post, string currentUserID);
        Task DeletePostAsync(int postId, string currentUserID);
    }
}

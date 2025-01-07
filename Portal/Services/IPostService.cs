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
        Task<Post> UpdatePostAsync(int postId, PostDTO dto, ClaimsPrincipal user);
        Task DeletePostAsync(int postId, ClaimsPrincipal user);
    }
}

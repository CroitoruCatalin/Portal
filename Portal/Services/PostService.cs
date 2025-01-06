using Portal.Models;
using Portal.Repositories;

namespace Portal.Services
{
    public class PostService : IPostService
    {
        private readonly IRepositoryWrapper _unitOfWork;

        public PostService(IRepositoryWrapper _unitOfWork)
        {
            _unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _unitOfWork.Posts.GetAllPostsAsync();
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await _unitOfWork.Posts.GetPostByIdAsync(postId);
        }

        public async Task CreatePostAsync(Post post)
        {
            await _unitOfWork.Posts.CreatePostAsync(post);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            await _unitOfWork.Posts.UpdatePostAsync(post);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePostAsync(int postId)
        {
            await _unitOfWork.Posts.DeletePostAsync(postId);
            await _unitOfWork.SaveAsync();
        }
    }
}

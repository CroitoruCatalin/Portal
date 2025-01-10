using Microsoft.AspNetCore.Identity;
using Portal.Models;
using Portal.Models.DTO;
using Portal.Repositories;
using System.Security.Claims;

namespace Portal.Services
{
    public class PostService : IPostService
    {
        private readonly IRepositoryWrapper _unitOfWork;
        private readonly UserManager<User> _userManager;

        public PostService(
            IRepositoryWrapper unitOfWork,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _unitOfWork.Posts.GetAllPostsAsync();
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await _unitOfWork.Posts.GetPostByIdAsync(postId);
        }

        public async Task<Post> CreatePostAsync(PostDTO dto, ClaimsPrincipal user)
        {
            var currentUser = await _userManager.GetUserAsync(user);
            
            if (currentUser == null) 
            {
                throw new InvalidOperationException("currentUser is null inside PostService.CreatePostAsync()");
            }
            var currentUserID = await _userManager.GetUserIdAsync(currentUser);

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Post object cannot be null");
            }

            Post newPost = new Post();
            dto.CreationDate = DateTime.UtcNow;
            newPost.SetFromDTO(dto);
            (newPost.AuthorID, newPost.Author) = (currentUser.Id, currentUser);

            await _unitOfWork.Posts.CreatePostAsync(newPost);
            await _unitOfWork.SaveAsync();

            return newPost;
        }

        public async Task<Post> UpdatePostAsync(int id, PostDTO post, ClaimsPrincipal user)
        {
            if (String.IsNullOrEmpty(post.Message))
            {
                throw new ArgumentNullException(nameof(post));
            }

            var currentUser = await _userManager.GetUserAsync(user);
            var currentUserID = await _userManager.GetUserIdAsync(currentUser);

            var postToBeUpdated = await _unitOfWork.Posts.GetPostByIdAsync(id);
            if (postToBeUpdated.AuthorID != currentUserID)
            {
                throw new UnauthorizedAccessException();
            }

            postToBeUpdated.Message = post.Message;

            await _unitOfWork.Posts.UpdatePostAsync(postToBeUpdated);
            await _unitOfWork.SaveAsync();
            return postToBeUpdated;
        }

        public async Task DeletePostAsync(int postId, ClaimsPrincipal user)
        {
            var currentUser = await _userManager.GetUserAsync(user);
            var postToBeDeleted = await _unitOfWork.Posts.GetPostByIdAsync(postId);
            if (currentUser == null || postToBeDeleted.AuthorID != currentUser.Id)
            {
                throw new UnauthorizedAccessException();
            }
            if (postToBeDeleted == null)
            {
                throw new InvalidOperationException();
            }

            await _unitOfWork.Posts.DeletePostAsync(postId);
            await _unitOfWork.SaveAsync();
        }
    }
}

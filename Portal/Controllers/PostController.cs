using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Portal.Models.DTO;
using Portal.Services;
using System.Security.Claims;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;
        private readonly UserManager<User> _userManager;


        public PostController(IPostService postService,
            ILogger<PostController> logger,
            UserManager<User> userManager)
        {
            _postService = postService;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostDTO dto)
        {
            Post createdPost = await _postService.CreatePostAsync(dto, User);

            return CreatedAtAction(nameof(GetPost), new { id = createdPost.PostID }, createdPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostDTO dto)
        {
            var updatedPost = await _postService.UpdatePostAsync(id, dto, User);
            return Ok(updatedPost);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id, User);
            return NoContent();
        }
    }
}

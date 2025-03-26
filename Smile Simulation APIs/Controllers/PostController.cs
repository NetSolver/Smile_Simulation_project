using Application.DTOs;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smile_Simulation_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
      
            private readonly PostService _postService;

            public PostController(PostService postService)
            {
                _postService = postService;
            }

          
            [HttpGet]
            public async Task<ActionResult<List<PostDTO>>> GetPosts([FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10, [FromQuery] int currentUserId = 0)
            {
                var posts = await _postService.GetAllPostsAsync(currentUserId);
                IReadOnlyList<PostDTO>paginatedPosts = posts
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(new
                {
                    TotalPosts = posts.Count,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = paginatedPosts
                });
            }

            
            [HttpGet("{postId}")]
            public async Task<ActionResult<PostDTO>> GetPostById(int postId, [FromQuery] int currentUserId = 0)
            {
                var post = await _postService.GetPostByIdAsync(postId, currentUserId);
                if (post == null) return NotFound("Post not found.");
                return Ok(post);
            }

         
            [HttpPost]
            public async Task<ActionResult<PostDTO>> CreatePost([FromBody] Post post)
            {
                if (post == null) return BadRequest("Invalid post data.");

                var createdPost = await _postService.AddPostAsync(post);
            //this line using to create url to display dto that return from the  add post by using method GetPostById
            return CreatedAtAction(nameof(GetPostById), new { postId = createdPost.Id }, createdPost);
            }

            [HttpPut("{postId}")]
            public async Task<ActionResult<PostDTO>> UpdatePost(int postId, [FromBody] Post updatedPost)
            {
                if (postId != updatedPost.Id) return BadRequest("Post ID mismatch.");

                var updated = await _postService.UpdatePostAsync(updatedPost);
                if (updated == null) return NotFound("Post not found.");

                return Ok(updated);
            }

      
            [HttpDelete("{postId}")]
            public async Task<ActionResult> DeletePost(int postId)
            {
                var deleted = await _postService.DeletePostAsync(postId);
                if (!deleted) return NotFound("Post not found.");

                return NoContent();
            }
        
    }
}

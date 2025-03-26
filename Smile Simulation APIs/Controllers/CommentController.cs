using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smile_Simulation_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

       
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentsByPostId(int postId)
        {
            var comments = await _commentService.GetAllCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

     
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CommentDTO commentDto)
        {
            if (commentDto == null) return BadRequest("Invalid comment data");

            var createdComment = await _commentService.AddCommentAsync(commentDto);
            return CreatedAtAction(nameof(GetCommentsByPostId), new { postId = createdComment.Id }, createdComment);
        }

        [HttpPut("{commentId}")]
        public async Task<ActionResult<CommentDTO>> UpdateComment(int commentId, [FromBody] string newContent)
        {
            var updatedComment = await _commentService.UpdateCommentAsync(commentId, newContent);
            if (updatedComment == null) return NotFound("Comment not found");

            return Ok(updatedComment);
        }

      
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var result = await _commentService.DeleteCommentAsync(commentId);
            if (!result) return NotFound("Comment not found");

            return NoContent();
        }
    }
}

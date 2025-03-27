using Application.DTOs;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smile_Simulation_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly CommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(CommentService commentService ,IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

       
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentsByPostId(int postId)
        {
            var comments = await _commentService.GetAllCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        [HttpPost("postId/{postId}")]
        public async Task<ActionResult<CommentDTO>> AddComment(int postId, [FromBody] CommentDTO commentDto)
        {
            if (commentDto == null) return BadRequest("Invalid comment data");

            commentDto.PostId = postId; 

            var createdComment = await _commentService.AddCommentAsync(commentDto);
            return CreatedAtAction(nameof(GetCommentsByPostId), new { postId = createdComment.PostId }, createdComment);
        }


        public class UpdateCommentRequest
        {
            public string NewContent { get; set; }
        }

        [HttpPut("postId/{postId}/CommentId/{commentId}")]
        public async Task<ActionResult<CommentDTO>> UpdateComment(int postId, int commentId, [FromBody] UpdateCommentRequest request)
        {
            if (string.IsNullOrEmpty(request.NewContent)) return BadRequest("Invalid content data.");

            var updatedComment = await _commentService.UpdateCommentAsync(postId, commentId, request.NewContent);
            if (updatedComment == null) return NotFound("Comment not found");

            return Ok(updatedComment);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var result = await _commentService.DeleteCommentAsync(commentId);
            if (!result) return NotFound("Comment not found");

            return Ok("Comment Deleted Successfully");
        }
    }
}

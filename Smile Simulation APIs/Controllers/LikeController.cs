using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smile_Simulation_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly LikeService _likeService;

        public LikeController(LikeService likeService)
        {
            _likeService = likeService;
        }

       
        [HttpPost("{postId}/user/{userId}")]
        public async Task<IActionResult> ToggleLike(int postId, int userId)
        {
            var isLiked = await _likeService.AddLikeAsync(postId, userId);

            if (isLiked)//AddLikeAsync bool function
                return Ok("Like added successfully");

            await _likeService.RemoveLikeAsync(postId, userId);
            return Ok("Like removed successfully");
        }
    }
}

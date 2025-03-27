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


        [HttpPost("postid{postId}/user/{userId}")]
        public async Task<IActionResult> ToggleLike(int postId, int userId)
        {
            var likeResult = await _likeService.AddLikeAsync(postId, userId);

            // إرسال استجابات مفهومة لـ Flutter حسب النتيجة
            if (likeResult == "Post not found")
                return NotFound(new { message = likeResult });

            if (likeResult == "User not found")
                return NotFound(new { message = likeResult });

            if (likeResult == "Already liked")
            {
                var removeResult = await _likeService.RemoveLikeAsync(postId, userId);
                return Ok(new { message = removeResult });  // إزالة الإعجاب إذا كان موجودًا مسبقًا
            }

            return Ok(new { message = likeResult });  // إضافة إعجاب بنجاح
        }

    }
}

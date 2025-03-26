using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public DateTime CreatedAt { get; set; }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PublisherProfileImageUrl { get; set; }

        public bool IsLikedByCurrentUser { get; set; }
    }


}

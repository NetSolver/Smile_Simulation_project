using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CommentDTO
    {
        public int CommentID { get; set; }
        public string Content { get; set; }
        public int  PostId { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PublisherImage { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}

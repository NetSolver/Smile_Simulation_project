using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Entities
{
    public class Post
    {
            public int Id { get; set; }
            public string Content { get; set; }
            public string? ImageUrl { get; set; }
            public int PublisherId { get; set; }
            public virtual User Publisher { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public virtual ICollection<Comment> Comments { get; set; } = new  List<Comment>();
            public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        

    }
}

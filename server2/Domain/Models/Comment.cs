using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Likes = new HashSet<Like>();
            Replies = new HashSet<Reply>();
        }

        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Comment1 { get; set; } = null!;

        public virtual Post Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Like
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public int? ReplyId { get; set; }

        public virtual Comment? Comment { get; set; }
        public virtual Post? Post { get; set; }
        public virtual Reply? Reply { get; set; }
        public virtual User User { get; set; } = null!;
    }
}

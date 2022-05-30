using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Comment1 { get; set; } = null!;

        public virtual Post Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

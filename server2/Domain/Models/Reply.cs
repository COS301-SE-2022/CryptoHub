using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Reply
    {
        public Reply()
        {
            Likes = new HashSet<Like>();
        }

        public int ReplyId { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public string Comment { get; set; } = null!;

        public virtual Comment CommentNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Like> Likes { get; set; }
    }
}

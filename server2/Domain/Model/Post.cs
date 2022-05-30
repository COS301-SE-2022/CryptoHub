using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public int PostId { get; set; }
        public string Post1 { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Like Like { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
    }
}

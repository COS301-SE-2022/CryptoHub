using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Post1 { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Tags = new HashSet<Tag>();
            PostReports = new HashSet<PostReport>();
        }

        public int PostId { get; set; }
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
        public int? ImageId { get; set; }

        public string? ImageUrl { get; set; } = null!;

        public virtual Image? Image { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<PostReport> PostReports { get; set; }
    }
}

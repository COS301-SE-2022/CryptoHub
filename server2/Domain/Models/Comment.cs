using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual Post? Post { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Like>? Likes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Reply>? Replies { get; set; }
    }
}

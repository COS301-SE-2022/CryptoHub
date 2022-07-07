using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
        }

        public int PostId { get; set; }
        public string Post1 { get; set; } = null!;
        public int UserId { get; set; }
        public int? ImageId { get; set; }

        [JsonIgnore]
        public virtual Image? Image { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment>? Comments { get; set; }

        [JsonIgnore]
        public virtual ICollection<Like>? Likes { get; set; }
    }
}

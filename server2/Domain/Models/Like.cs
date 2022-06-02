using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class Like
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public int? ReplyId { get; set; }

        [JsonIgnore]
        public virtual Comment? Comment { get; set; }

        [JsonIgnore]
        public virtual Post? Post { get; set; }

        [JsonIgnore]
        public virtual Reply? Reply { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}

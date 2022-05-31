using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class UserFollower
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowId { get; set; }
        public DateTime FollowDate { get; set; }

        [JsonIgnore]
        public virtual User Follow { get; set; } = null!;

        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}

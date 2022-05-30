using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class UserFollower
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowId { get; set; }
        public DateTime FollowDate { get; set; }

        public virtual User Follow { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

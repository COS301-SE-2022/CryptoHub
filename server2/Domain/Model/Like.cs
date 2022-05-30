using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public partial class Like
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public virtual Post LikeNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

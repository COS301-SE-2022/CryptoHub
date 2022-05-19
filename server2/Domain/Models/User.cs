using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
            UserFollowerFollows = new HashSet<UserFollower>();
            UserFollowerUsers = new HashSet<UserFollower>();
        }

        public int UserId { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserFollower> UserFollowerFollows { get; set; }
        public virtual ICollection<UserFollower> UserFollowerUsers { get; set; }
    }
}

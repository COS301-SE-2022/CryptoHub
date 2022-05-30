using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual ICollection<Post>? Posts { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<UserFollower>? UserFollowerFollows { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserFollower>? UserFollowerUsers { get; set; }
    }
}

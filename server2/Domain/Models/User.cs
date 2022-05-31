using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Posts = new HashSet<Post>();
            UserCoins = new HashSet<UserCoin>();
            UserFollowerFollows = new HashSet<UserFollower>();
            UserFollowerUsers = new HashSet<UserFollower>();
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; }

        [JsonIgnore]
        public virtual ICollection<Like> Likes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserCoin> UserCoins { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserFollower> UserFollowerFollows { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserFollower> UserFollowerUsers { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

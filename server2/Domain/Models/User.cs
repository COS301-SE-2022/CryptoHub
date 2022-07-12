using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Posts = new HashSet<Post>();
            Replies = new HashSet<Reply>();
            UserCoins = new HashSet<UserCoin>();
            UserFollowerFollows = new HashSet<UserFollower>();
            UserFollowerUsers = new HashSet<UserFollower>();
            UserRoles = new HashSet<UserRole>();
            CoinRatings = new HashSet<CoinRating>();
        }

        public int UserId { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int OTP { get; set; }
        public DateTime OTPExpirationTime { get; set; }
        public bool HasForgottenPassword { get; set; }

        public int? ImageId { get; set; }
        public int RoleId { get; set; }

        public virtual Image? Image { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<UserCoin> UserCoins { get; set; }
        public virtual ICollection<UserFollower> UserFollowerFollows { get; set; }
        public virtual ICollection<UserFollower> UserFollowerUsers { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<CoinRating> CoinRatings { get; set; }
    }
}

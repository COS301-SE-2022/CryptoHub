using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
            Follows = new HashSet<User>();
            Users = new HashSet<User>();
        }

        public int UserId { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<User> Follows { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

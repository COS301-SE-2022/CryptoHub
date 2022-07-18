using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Image
    {
        public Image()
        {
            Coins = new HashSet<Coin>();
            Posts = new HashSet<Post>();
            Users = new HashSet<User>();
        }

        public int ImageId { get; set; }
        public byte[] Blob { get; set; } = null!;

        public virtual ICollection<Coin> Coins { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

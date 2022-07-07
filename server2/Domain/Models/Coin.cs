using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Coin
    {
        public Coin()
        {
            UserCoins = new HashSet<UserCoin>();
        }

        public int CoinId { get; set; }
        public string CoinName { get; set; } = null!;
        public int? ImageId { get; set; }

        public virtual Image? Image { get; set; }
        public virtual ICollection<UserCoin> UserCoins { get; set; }
    }
}

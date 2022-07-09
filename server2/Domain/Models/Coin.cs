using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Coin
    {
        public Coin()
        {
            UserCoins = new HashSet<UserCoin>();
            CoinRatings = new HashSet<CoinRating>();
        }

        public int CoinId { get; set; }
        public string CoinName { get; set; } = null!;
        public int? ImageId { get; set; }

        public virtual Image? Image { get; set; }
        public virtual ICollection<UserCoin> UserCoins { get; set; }

        public virtual ICollection<CoinRating> CoinRatings { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public partial class UserCoin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CoinId { get; set; }

        public virtual Coin Coin { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

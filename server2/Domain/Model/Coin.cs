using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public partial class Coin
    {
        public Coin()
        {
            CoinHistories = new HashSet<CoinHistory>();
            UserCoins = new HashSet<UserCoin>();
        }

        public int CoinId { get; set; }
        public string CoinName { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public int? Rank { get; set; }
        public decimal? TradingPriceUsd { get; set; }
        public decimal? PercentageChange { get; set; }
        public decimal? Supply { get; set; }
        public decimal? MaxSupply { get; set; }
        public decimal? MarketCapUsd { get; set; }

        public virtual ICollection<CoinHistory> CoinHistories { get; set; }
        public virtual ICollection<UserCoin> UserCoins { get; set; }
    }
}

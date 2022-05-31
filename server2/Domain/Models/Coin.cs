using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
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

        [JsonIgnore]
        public virtual ICollection<CoinHistory> CoinHistories { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserCoin> UserCoins { get; set; }
    }
}

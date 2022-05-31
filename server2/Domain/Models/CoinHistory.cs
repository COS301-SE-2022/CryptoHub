using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class CoinHistory
    {
        public int HistoryId { get; set; }
        public int CoinId { get; set; }
        public int? Rank { get; set; }
        public byte[] Timestamp { get; set; } = null!;
        public decimal? TradingPriceUsd { get; set; }
        public decimal? PercentageChange { get; set; }
        public decimal? Supply { get; set; }
        public decimal? MaxSupply { get; set; }
        public decimal? MarketCapUsd { get; set; }

        [JsonIgnore]
        public virtual Coin Coin { get; set; } = null!;
    }
}

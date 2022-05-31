using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class UserCoin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CoinId { get; set; }

        [JsonIgnore]
        public virtual Coin Coin { get; set; } = null!;

        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CoinRating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CoinId { get; set; }

        public int Rating { get; set; }

        public virtual Coin Coin { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

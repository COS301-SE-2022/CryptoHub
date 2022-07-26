using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.CoinDTOs
{
    public class CoinDTO
    {
        public int CoinId { get; set; }
        public string CoinName { get; set; } = null!;
        public string? ImageUrl { get; set; } = null!;

    }
}

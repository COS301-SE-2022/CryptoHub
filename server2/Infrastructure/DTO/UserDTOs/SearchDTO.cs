using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.UserDTOs
{
    public class SearchDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
    }
}

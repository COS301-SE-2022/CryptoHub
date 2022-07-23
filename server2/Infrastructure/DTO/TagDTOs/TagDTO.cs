using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.TagDTOs
{
    public class TagDTO
    {
        public int TagId { get; set; }
        public int PostId { get; set; }
        public string? Content { get; set; }
    }
}

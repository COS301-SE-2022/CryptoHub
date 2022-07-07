using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.CommentDTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; } = null!;
    }
}

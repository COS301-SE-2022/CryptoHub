using System;
namespace Infrastructure.DTO.ReplyDTOs
{
    public class ReplyDTO
    {
        public int ReplyId { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; } = null!;
    }
}


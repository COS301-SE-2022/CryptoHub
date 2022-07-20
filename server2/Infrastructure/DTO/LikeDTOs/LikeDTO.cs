using System;
namespace Infrastructure.DTO.LikeDTOs
{
    public class LikeDTO
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
    }
}


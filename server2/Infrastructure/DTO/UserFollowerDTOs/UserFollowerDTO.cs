using System;
namespace Infrastructure.DTO.UserFollowerDTOs
{
    public class UserFollowerDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowId { get; set; }
        public DateTime FollowDate { get; set; }
    }
}


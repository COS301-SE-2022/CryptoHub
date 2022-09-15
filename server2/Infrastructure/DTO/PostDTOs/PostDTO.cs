using System;
namespace Infrastructure.DTO.PostDTO
{
    public class PostDTO
    {

        public int PostId { get; set; }
        public string Content { get; set; } = null!;
        public int UserId { get; set; }

        public string? ImageUrl { get; set; } = null!;

        public decimal? SentimentScore { get; set; }


    }
}


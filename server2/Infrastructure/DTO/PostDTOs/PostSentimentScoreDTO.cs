using System;
using Infrastructure.DTO.ImageDTOs;
using Infrastructure.DTO.TagDTOs;

namespace Infrastructure.DTO.PostDTO
{
    public class PostSentimentScoreDTO
    { 
        public int PostId { get; set; }
        public float SentimentScore { get; set; }
    }
}

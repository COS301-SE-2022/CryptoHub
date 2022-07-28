using System;
namespace Infrastructure.DTO.ReportPostDTO
{
    public class ReportPostDTO
    {
        public int PostId { get; set; }
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
        public int ReportCount { get; set; }
        public string? ImageUrl { get; set; } = null!;
    }
}


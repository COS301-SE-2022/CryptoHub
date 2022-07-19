using System;
using Infrastructure.DTO.ImageDTOs;
namespace Infrastructure.DTO.PostDTO
{
    public class CreatePostDTO
    {
        public string Post { get; set; } = null!;
        public int UserId { get; set; }

        public CreateImageDTO? ImageDTO { get; set; }
    }
}

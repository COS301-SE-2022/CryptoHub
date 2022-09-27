using System;
using Infrastructure.DTO.ImageDTOs;
using Infrastructure.DTO.TagDTOs;

namespace Infrastructure.DTO.PostDTO
{
    public class CreatePostDTO
    {
        public string Post { get; set; } = null!;
        public int UserId { get; set; }

        public CreateImageDTO? ImageDTO { get; set; }

        public BatchTagsDTO? BatchTags { get; set; }
    }
}

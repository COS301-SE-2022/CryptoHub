using System;
using Infrastructure.DTO.imageDTO;
namespace Infrastructure.DTO.PostDTO
{
    public class CreatePostDTO
    {
        public string Post { get; set; } = null!;
        public int UserId { get; set; }

        public imageDTO? imageDTO { get; set; }
    }
}

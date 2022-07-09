using System;
using Domain.Models;
using Infrastructure.DTO.PostDTO;


namespace BusinessLogic.Services.PostService
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetAllPosts();
        Task<PostDTO> GetPostByUserId(int id);
        Task<PostDTO> AddPost(CreatePostDTO createPostDTO); //Ask khotso
        Task<PostDTO> UpdatePost(Post Post);
        Task<PostDTO> Delete(int id);

    }
}


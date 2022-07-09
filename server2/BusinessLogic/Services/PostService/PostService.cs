using System;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.PostDTO;

namespace BusinessLogic.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IImageRepository _imageRepository;
        public PostService(IPostRepository postRepository, IImageRepository imageRepository)
        {
            _postRepository = postRepository;
            _imageRepository = imageRepository;
        }

        public Task<PostDTO> AddPost(CreatePostDTO createPostDTO)
        {
          
        }

        public Task<PostDTO> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostDTO>> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public Task<PostDTO> GetPostByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PostDTO> UpdatePost(Post Post)
        {
            throw new NotImplementedException();
        }
    }
}


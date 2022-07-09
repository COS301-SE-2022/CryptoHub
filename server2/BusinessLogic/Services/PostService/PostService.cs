using System;
using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.PostDTO;

namespace BusinessLogic.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, IImageRepository imageRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<List<PostDTO>> GetAllPosts()
        {
            var Post = await _postRepository.GetAll();
            return _mapper.Map<List<PostDTO>>(Post);

        }
        public async Task<PostDTO> GetPostByUserId(int id)
        {

        }

        public async Task<PostDTO> AddPost(CreatePostDTO createPostDTO)
        {
          
        }
        public async Task<PostDTO> UpdatePost(Post Post)
        {

        }
        public async Task<PostDTO> Delete(int id)
        {
            
        }

        

        

        
    }
}


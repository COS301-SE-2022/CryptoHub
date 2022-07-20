using System;
using AutoMapper;
using BusinessLogic.Services.ImageService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.PostDTO;

namespace BusinessLogic.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, IImageService imageService, IMapper mapper)
        {
            _postRepository = postRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<List<PostDTO>> GetAllPosts()
        {
            var Post = await _postRepository.GetAll();
            return _mapper.Map<List<PostDTO>>(Post);
        }
        public async Task<List<PostDTO>> GetPostByUserId(int id)
        {
            var response = await _postRepository.FindRange(p => p.UserId == id);
            if (response == null)
                return null;

            return _mapper.Map<List<PostDTO>>(response);

        }

        public async Task<PostDTO> AddPost(CreatePostDTO createPostDTO)
        {
            Post post = new Post();

            post.Content = createPostDTO.Post;
            post.UserId = createPostDTO.UserId;

            await _postRepository.Add(post);

            if (createPostDTO.ImageDTO != null)
            {
                createPostDTO.ImageDTO.Name = $"post-{post.PostId}";
                await _imageService.AddImage(createPostDTO.ImageDTO);
            }

          
            return _mapper.Map<PostDTO>(post);

        }

        public async Task<PostDTO> UpdatePost(Post Post)
        {
            var response = await _postRepository.Update(u => u.PostId == Post.PostId, Post);
            if (response == null)
                return null;

            return _mapper.Map<PostDTO>(response);
        }
        public async Task Delete(int id)
        {
            await _postRepository.DeleteOne(u => u.PostId == id);

        }






    }
}


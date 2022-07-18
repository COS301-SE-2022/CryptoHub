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
            var response = await _postRepository.FindRange(p => p.UserId == id);
            if (response == null)
                return null;

            return _mapper.Map<PostDTO>(response);

        }

        public async Task<PostDTO> AddPost(CreatePostDTO createPostDTO)
        {
            Post post = new Post();
            if (createPostDTO.ImageDTO != null)
            {
                byte[] imageArray = Convert.FromBase64String(createPostDTO.ImageDTO.Blob);

                Image image = new Image();
                image.Blob = imageArray;

                await _imageRepository.Add(image);
                post.ImageId = image.ImageId;

            }

            post.Content = createPostDTO.Post;
            post.UserId = createPostDTO.UserId;

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


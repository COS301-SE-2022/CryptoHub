using System;
using AutoMapper;
using BusinessLogic.Services.ImageService;
using BusinessLogic.Services.TagService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.PostDTO;
using Infrastructure.DTO.ReportPostDTO;

namespace BusinessLogic.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IImageService _imageService;
        private readonly IPostReportRepository _postReportRepository;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, IImageService imageService, IPostReportRepository postReportRepository, IMapper mapper, ITagService tagService)
        {
            _postRepository = postRepository;
            _imageService = imageService;
            _postReportRepository = postReportRepository;
            _tagService = tagService;
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
                var response = await _imageService.AddImage(createPostDTO.ImageDTO);

                if (response.HasError)
                    return null;

                post.ImageId = response.Model.ImageId;
                post.ImageUrl = response.Model.Url;
                await _postRepository.Update(post);
            }

            if (createPostDTO.BatchTags != null)
            {
                var response = await _tagService.BatchAddTag(post.PostId, createPostDTO.BatchTags);

                if (response.HasError)
                    return null;
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

        public async Task<PostReport> Report(int postid, int userid)
        {
            var CheckpostReport = await _postReportRepository.GetByExpression(p => p.PostId == postid && p.UserId == userid);
            if (CheckpostReport != null)
            {
                return null;
            }

            var newReport = new PostReport
            {
                PostId = postid,
                UserId = userid
            };

            await _postReportRepository.Add(newReport);

            return newReport;
        }

        public async Task<Response<object>> GetReportCountByPostId(int id)
        {
            var response = await _postReportRepository.FindRange(c => c.PostId == id);
            if (response == null)
                return new Response<object>(null, true, "no reports");

            return new Response<object>(new { Count = response.Count() }, false, "");

        }

        public async Task<IEnumerable<ReportPostDTO>> GetAllReportedPosts()
        {
            var reports = await _postReportRepository.GetAll();
            var allPosts = await _postRepository.GetAll();

            var reportedPosts = from r in reports
                                join p in allPosts
                                on r.PostId equals p.PostId
                                select new ReportPostDTO
                                {
                                    PostId = p.PostId,
                                    Content = p.Content,
                                    UserId = p.UserId,
                                    ImageUrl = p.ImageUrl,
                                    ReportCount = 0
                                };
            var final = new List<ReportPostDTO>();
            foreach (var post in reportedPosts)
            {
                var response = await _postReportRepository.FindRange(c => c.PostId == post.PostId);
                
                var count = response.Count();
                var temp = new ReportPostDTO
                {
                    PostId = post.PostId,
                    Content = post.Content,
                    UserId = post.UserId,
                    ImageUrl = post.ImageUrl,
                    ReportCount = count
                };
                final.Add(temp);
            }
            final = final.GroupBy(x => x.PostId).Select(x => x.First()).ToList();


            return _mapper.Map<IEnumerable<ReportPostDTO>>(final);
        }
    }
}


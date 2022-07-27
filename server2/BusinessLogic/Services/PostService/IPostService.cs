using System;
using Domain.Models;
using Infrastructure.DTO.PostDTO;


namespace BusinessLogic.Services.PostService
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetAllPosts();
        Task<List<PostDTO>> GetPostByUserId(int id);
        Task<PostDTO> AddPost(CreatePostDTO createPostDTO);
        Task<PostDTO> UpdatePost(Post Post);
        Task Delete(int id);
        Task<List<PostReport>> GetAllReports();
        Task<PostReport> Report(int postid, int userid);
        Task<Response<object>> GetReportCountByPostId(int id);
        Task<IEnumerable<PostDTO>> GetAllReportedPosts();
    }
}


using System;
using Domain.Models;
using Infrastructure.DTO.LikeDTOs;

namespace BusinessLogic.Services.LikeService
{
    public interface ILikeService
    {
        Task<List<LikeDTO>> GetAllLikes();

        Task<List<LikeDTO>> GetLikeByUserId(int id);

        Task<List<LikeDTO>> GetLikeByPostId(int id);


       Task<Response<object>> GetLikeCountByPostId(int id);

        Task<List<LikeDTO>> GetLikeByCommentId(int id);

        Task<Response<object>> GetLikeCountByCommentId(int id);

        Task<List<LikeDTO>> GetLikeByReplyId(int id);

        Task<Response<object>> GetLikeCountByReplyId(int id);

        Task<LikeDTO> GetLikeBy(int userId, int postId);

        Task<LikeDTO> AddLike(LikeDTO like);

        Task<LikeDTO> UpdateLike(Like like);

        Task Delete(int userId, int postId);



    }
}


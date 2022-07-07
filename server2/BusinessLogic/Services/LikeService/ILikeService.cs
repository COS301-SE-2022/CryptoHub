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

        Task<LikeDTO> GetLikeCountByPostId(int id);

        Task<List<LikeDTO>> GetLikeByCommentId(int id);

        Task<LikeDTO> GetLikeCountByCommentId(int id);

        Task<List<LikeDTO>> GetLikeByReplyId(int id);

        Task<LikeDTO> GetLikeCountByReplyId(int id);

        Task<LikeDTO> GetLikeBy(int userId, int postId);

        Task<LikeDTO> AddLike(Like like);

        Task<LikeDTO> UpdateLike(Like like);

        Task<LikeDTO> Delete(int id);



    }
}


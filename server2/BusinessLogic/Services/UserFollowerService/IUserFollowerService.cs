using System;
using Domain.Models;
using Infrastructure.DTO.UserFollowerDTOs;

namespace BusinessLogic.Services.UserFollowerService
{
    public interface IUserFollowerService
    {

        Task<List<UserFollowerDTO>> GetAllUserFollowers();
        Task<List<UserFollowerDTO>> GetUserUserFollower(int id);
        Task<List<UserFollowerDTO>> GetUserFollowing(int id);
        Task<Response<string>> FollowUser(int userid, int targetid);
        Task<Response<string>> UnfollowUser(int userId, int followId);
    }
}


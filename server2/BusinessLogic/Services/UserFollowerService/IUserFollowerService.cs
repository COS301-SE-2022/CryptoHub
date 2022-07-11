using System;
using Infrastructure.DTO.UserFollowerDTOs;

namespace BusinessLogic.Services.UserFollowerService
{
    public interface IUserFollowerService
    {

        Task<List<UserFollowerDTO>> GetAllUserFollowers();
        Task<UserFollowerDTO> GetUserUserFollower(int id);
        Task<UserFollowerDTO> GetUserFollowing(int id);
        Task<UserFollowerDTO> FollowUser(int userid, int targetid);


    }
}


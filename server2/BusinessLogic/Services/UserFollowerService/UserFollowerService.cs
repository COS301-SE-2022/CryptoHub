using System;
using AutoMapper;
using Domain.IRepository;
using Infrastructure.DTO.UserFollowerDTOs;

namespace BusinessLogic.Services.UserFollowerService
{
    public class UserFollowerService : IUserFollowerService
    {
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserFollowerService(IUserFollowerRepository userFollowerRepository, IUserRepository userRepository,  IMapper mapper )
        {
            _userFollowerRepository = userFollowerRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<UserFollowerDTO> FollowUser(int userid, int targetid)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserFollowerDTO>> GetAllUserFollowers()
        {
            throw new NotImplementedException();
        }

        public Task<UserFollowerDTO> GetUserFollowing(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserFollowerDTO> GetUserUserFollower(int id)
        {
            throw new NotImplementedException();
        }
    }
}


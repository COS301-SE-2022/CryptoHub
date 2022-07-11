using System;
using AutoMapper;
using Domain.IRepository;
using Domain.Models;
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

        public async Task<List<UserFollowerDTO>> GetAllUserFollowers()
        {
            var user = await _userFollowerRepository.GetAll();
            return _mapper.Map<List<UserFollowerDTO>>(user);
        }

        public async Task<IEnumerable<UserFollowerDTO>> GetUserUserFollower(int id)
        {
            var followers = await _userFollowerRepository.FindRange(uf => uf.UserId == id);
            var users = await _userRepository.GetAll();



            var userfollowers = from f in followers
                                join u in users
                                on f.FollowId equals u.UserId
                                select new UserFollowerDTO
                                {
                                    Id = f.Id,
                                    UserId = u.UserId,
                                    FollowId = f.FollowId,
                                    FollowDate = f.FollowDate
                                };

            return _mapper.Map<IEnumerable<UserFollowerDTO>>(userfollowers);
        }
        public async Task<IEnumerable<UserFollowerDTO>> GetUserFollowing(int id)
        {
            var followers = await _userFollowerRepository.FindRange(uf => uf.FollowId == id);
            var users = await _userRepository.GetAll();



            var userfollowers = from f in followers
                                join u in users
                                on f.UserId equals u.UserId
                                select new UserFollowerDTO
                                {
                                    Id = f.Id,
                                    UserId = u.UserId,
                                    FollowId = f.FollowId,
                                    FollowDate = f.FollowDate
                                };

            return _mapper.Map<IEnumerable<UserFollowerDTO>>(userfollowers);
        }

        public async Task<Response<string>> FollowUser(int userid, int targetid)
        {
            var response = await _userFollowerRepository.FindOne(uf => uf.UserId == userid && uf.FollowId == targetid);

            if (response != null)
                return null;

            UserFollower userFollower = new UserFollower
            {
                UserId = userid,
                FollowId = targetid,
                FollowDate = DateTime.Now
            };

            await _userFollowerRepository.Add(userFollower);

            return new Response<string>(null, true, "User already followed by that user");


        }






    }
}


using System;
using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.UserFollowerDTOs;
using BusinessLogic.Services.UserService;
using Microsoft.AspNetCore.Http;
using Infrastructure.DTO.PostDTO;

namespace BusinessLogic.Services.UserFollowerService
{
    public class UserFollowerService : IUserFollowerService
    {
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public UserFollowerService(IUserFollowerRepository userFollowerRepository, IUserRepository userRepository, IMapper mapper, IUserService userService, IHttpContextAccessor httpContextAccessor, IPostRepository postRepository)
        {
            _userFollowerRepository = userFollowerRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _postRepository = postRepository;
        }

        public async Task<List<UserFollowerDTO>> GetAllUserFollowers()
        {
            var user = await _userFollowerRepository.GetAll();
            return _mapper.Map<List<UserFollowerDTO>>(user);
        }

        public async Task<List<UserFollowerDTO>> GetUserUserFollower(int id)
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

            return _mapper.Map<List<UserFollowerDTO>>(userfollowers.ToList());
        }
        public async Task<List<UserFollowerDTO>> GetUserFollowing(int id)
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

            return _mapper.Map<List<UserFollowerDTO>>(userfollowers.ToList());
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

        public async Task<Response<string>> UnfollowUser(int userId, int followId)
        {
            var user = await _userService.GetById(userId);

            if (user == null)
                return new Response<string>(null, true, "User does not exist");

            var response = await _userFollowerRepository.GetByExpression(uf => uf.UserId == userId && uf.FollowId == followId);

            if (response == null)
                return new Response<string>(null, true, "User not followed by that user");

            await _userFollowerRepository.DeleteOne(u => u.UserId == userId && u.FollowId == followId);
            return new Response<string>(null, false, "User has been unfollowed");

        }

        public async Task<Response<List<PostDTO>>> GetFeed()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;

            var user = await _userRepository.GetByExpression(u => u.UserId.ToString() == userId);

            if (user == null)
                return new Response<List<PostDTO>>(null, true, "user not found");

            var userFollowers = await _userFollowerRepository.ListByExpression(u => u.UserId == user.UserId);
            var users = await _userRepository.GetAll();
            var posts = await _postRepository.GetAll();



            var feed = (from uf in userFollowers
                       join u in users
                       on uf.FollowId equals u.UserId
                       join p in posts
                       on u.UserId equals p.UserId
                       select new PostDTO
                       {
                           
                              UserId = u.UserId,
                              PostId = p.PostId,
                              Content = p.Content,
                              ImageUrl = p.ImageUrl
                              
                       }).ToList();

            
             return new Response<List<PostDTO>>(feed, false, "");

        }
    }
}


using AutoMapper;
using BusinessLogic.Services.ImageService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.DTO.ImageDTOs;
using Infrastructure.DTO.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BusinessLogic.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly CryptoHubDBContext _dBContext;

        public UserService(IUserRepository userRepository, IImageService imageService, IHttpContextAccessor httpContextAccessor, IMapper mapper, IUserFollowerRepository userFollowerRepository, CryptoHubDBContext dBContext)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userFollowerRepository = userFollowerRepository;
            _dBContext = dBContext;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<UserDTO>>(users);

        }

        public async Task<UserDTO> GetById(int id)
        {
            //var response = await _userRepository.GetById(u => u.UserId == id);
            var response = await _dBContext.Users.FromSqlRaw("select * from getuser({0})",id).FirstOrDefaultAsync();
            if (response == null)
                return null;

            return _mapper.Map<UserDTO>(response);
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var response = await _userRepository.FindOne(u => u.Email == email);
            if (response == null)
                return null;

            return _mapper.Map<UserDTO>(response);
        }

        public async Task<UserDTO> AddUser(User user)
        {
            var response = await _userRepository.FindOne(u => u.Email == user.Email);
            if (response == null)
                return null;

            await _userRepository.Add(user);

            return _mapper.Map<UserDTO>(user);

        }

        public async Task<UserDTO> UpateUser(User user)
        {
            var response = await _userRepository.Update(u => u.UserId == user.UserId, user);
            if (response == null)
                return null;

            return _mapper.Map<UserDTO>(user);
        }

        public async Task Delete(int id)
        {
            await _userRepository.DeleteOne(u => u.UserId == id);
        }
        public async Task<List<SearchDTO>> SuggestedUsers(int id)
        {
            
            /*var followers = await _userFollowerRepository.FindRange(uf => uf.FollowId == id);
            var users = await _userRepository.GetAll();

            var resultList = new List<SearchDTO>();

            foreach (var result in users.ToList())
            {
                var temp = new SearchDTO
                {
                    UserId = result.UserId,
                    Firstname = result.Firstname,
                    Lastname = result.Lastname,
                    Username = result.Username,
                    followCount = 0
                };
                resultList.Add(temp);
            }

            foreach (var r in resultList.ToList()) //order by follow count
            {
                var fol = await _userFollowerRepository.FindRange(uf => uf.UserId == r.UserId);
                var allUsers = await _userRepository.GetAll();



                var userFol = from f in fol
                              join u in allUsers
                              on f.FollowId equals u.UserId
                              select new
                              {
                                  UserId = u.UserId,
                                  Username = u.Username
                              };
                r.followCount = userFol.Count();
            }

            resultList = resultList.OrderByDescending(r => r.followCount).ToList();

            var userfollowers = from f in followers //All users user follows
                                join u in resultList
                                on f.UserId equals u.UserId
                                select new SearchDTO
                                {
                                    UserId = u.UserId,
                                    Firstname = u.Firstname,
                                    Lastname = u.Lastname,
                                    Username = u.Username,
                                };


            var mutuals = new List<SearchDTO>();
            foreach (var usf in userfollowers.ToList()) //gets all mutual users
            {
                var mutFollowers = await _userFollowerRepository.FindRange(uf => uf.FollowId == usf.UserId);
                var mutUsers = await _userRepository.GetAll();

                var mutUserfollowers = from f in mutFollowers
                                       join u in mutUsers
                                    on f.UserId equals u.UserId
                                       select new SearchDTO
                                       {
                                           UserId = u.UserId,
                                           Firstname = u.Firstname,
                                           Lastname = u.Lastname,
                                           Username = u.Username,
                                       };


                foreach (var m in mutUserfollowers.ToList())
                {
                    if (m.UserId != id)
                    {
                        mutuals.Add(m);
                    }
                }
            }
            

            foreach (var user in resultList.ToList())
            {
                if (user.UserId != id)
                {
                    mutuals.Add(user);
                }
            }
            foreach (var user in userfollowers.ToList())
            {
                foreach (var m in mutuals.ToList())
                {
                    if (m.UserId == user.UserId)
                    {
                        mutuals.Remove(m);
                    }
                }
            }
            mutuals = mutuals.GroupBy(x => x.UserId).Select(x => x.First()).ToList();
            var finalList = new List<SearchDTO>();
            for (int i = 0; i < 5; i++)
            {
                finalList.Add(mutuals.ElementAt(i));
            }*/

            var finalList = await _dBContext.Users.FromSqlRaw("SELECT* FROM suggesteduser({0})", id).ToListAsync();
            return _mapper.Map<List<SearchDTO>>(finalList);
        }

        public async Task<Response<string>> UploadProfilePic(CreateImageDTO createImageDTO)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;

            var user = await _userRepository.GetByExpression(u => u.UserId.ToString() == userId);

            if (user == null)
                return new Response<string>(null, true, "user not found");

            createImageDTO.Name = $"user-{userId}";

            var response = await _imageService.AddImage(createImageDTO);

            if (response.HasError)
                return new Response<string>(null, true, response.Message);

            user.ImageId = response.Model.ImageId;
            user.ImageUrl = response.Model.Url;

            await _userRepository.Update(user);

            return new Response<string>(null, false, "profile uploaded");
        }

        

    }
}

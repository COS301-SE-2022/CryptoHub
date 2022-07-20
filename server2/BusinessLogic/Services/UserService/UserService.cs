using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.UserDTOs;

namespace BusinessLogic.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IUserFollowerRepository userFollowerRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userFollowerRepository = userFollowerRepository;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<UserDTO>>(users);

        }

        public async Task<UserDTO> GetById(int id)
        {
            var response = await _userRepository.GetById(u => u.UserId == id);
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
            var followers = await _userFollowerRepository.FindRange(uf => uf.FollowId == id);
            var users = await _userRepository.GetAll();

            var userfollowers = from f in followers
                                join u in users
                                on f.UserId equals u.UserId
                                select new User
                                {
                                    UserId = u.UserId,
                                    Firstname = u.Firstname,
                                    Lastname = u.Lastname,
                                    Username = u.Username,
                                };

            var mutuals = new List<User>();
            foreach (var usf in userfollowers.ToList())
            {
                var mutFollowers = await _userFollowerRepository.FindRange(uf => uf.FollowId == usf.UserId);
                var mutUsers = await _userRepository.GetAll();

                var mutUserfollowers = from f in mutFollowers
                                       join u in mutUsers
                                    on f.UserId equals u.UserId
                                       select new User
                                       {
                                           UserId = u.UserId,
                                           Firstname = u.Firstname,
                                           Lastname = u.Lastname,
                                           Username = u.Username,
                                       };

                
                foreach (var m in mutUserfollowers.ToList())
                {
                    mutuals.Add(m);
                }
            }
            mutuals = mutuals.GroupBy(x => x.UserId).Select(x => x.First()).ToList();
            return _mapper.Map<List<SearchDTO>>(mutuals);
        }

    }
}

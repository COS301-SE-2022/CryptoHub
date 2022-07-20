using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.UserDTOs;

namespace BusinessLogic.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

    }
}

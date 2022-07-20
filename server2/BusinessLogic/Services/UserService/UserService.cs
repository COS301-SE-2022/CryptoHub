using AutoMapper;
using BusinessLogic.Services.ImageService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.ImageDTOs;
using Infrastructure.DTO.UserDTOs;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BusinessLogic.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IImageService imageService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<Response<string>> UploadProfilePic(CreateImageDTO createImageDTO)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
            
            var user = await _userRepository.GetByExpression(u => u.UserId.ToString() == userId);

            if (user == null)
                return new Response<string>(null, true, "user not found");

            createImageDTO.Name = $"user-{userId}";

            var response = await _imageService.AddImage(createImageDTO);

            if(response.HasError)
                return new Response<string>(null, true, response.Message);

            user.ImageId = response.Model.ImageId;

            await _userRepository.Update(user);

            return new Response<string>(null, false, "profile uploaded");




        }

    }
}

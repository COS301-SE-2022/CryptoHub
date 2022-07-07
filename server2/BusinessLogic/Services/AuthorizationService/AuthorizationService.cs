using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.AuthorizationService
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthorizationService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<UserDTO>> Login(LoginDTO loginDTO)
        {
            var loginUser = await _userRepository.FindOne(u => u.Email == loginDTO.Email);

            if (loginUser == null)
                return new Response<UserDTO>(null, true, "incorrect username or password");

            if (!(loginUser.Password == loginDTO.Password))
                return new Response<UserDTO>(null, true, "incorrect username or password");

            var user = _mapper.Map<UserDTO>(loginUser);
            return new Response<UserDTO>(user, false, "logged in");
        }

        public async Task<Response<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var registerUser = await _userRepository.FindOne(u => u.Email == registerDTO.Email);

            if (registerUser != null)
                return new Response<UserDTO>(null, true, "user already exists");
                
            var user = _mapper.Map<User>(registerDTO);
            await _userRepository.Add(user);

            var response = _mapper.Map<UserDTO>(user);

            return new Response<UserDTO>(response, false, "registered");



    }
}

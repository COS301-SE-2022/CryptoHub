using AutoMapper;
using BusinessLogic.Services.RoleServices;
using BusinessLogic.Services.UserService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.EmailDTOs;
using Infrastructure.DTO.UserDTOs;
using Intergration.SendGridEmailService;
using Intergration.SendInBlueEmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.AuthorizationService
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly IConfiguration _configuration;
        private readonly ISendInBlueEmailService _sendInBlueEmailService;
        private readonly IMapper _mapper;

        public AuthorizationService(
            IUserRepository userRepository, IRoleService roleService
            , IConfiguration configuration, ISendInBlueEmailService sendInBlueEmailService
            , IMapper mapper)
        {
            _userRepository = userRepository;
            _roleService = roleService;
            _configuration = configuration;
            _sendInBlueEmailService = sendInBlueEmailService;
            _mapper = mapper;
        }

        public async Task<Response<JWT>> Login(LoginDTO loginDTO)
        {

            var loginUser = await _userRepository.GetByExpression(u => u.Email.ToLower() == loginDTO.Email.ToLower());

            if (loginUser == null)
                return new Response<JWT>(null, true, "incorrect username or password");

            if (!(loginUser.Password == loginDTO.Password))
                return new Response<JWT>(null, true, "incorrect username or password");


            var role = await _roleService.GetRoleById(loginUser.RoleId);

            var token = CreateToken(loginUser,role.Name);
            return new Response<JWT>(token, false, "logged in");
        }

        public async Task<Response<JWT>> Register(RegisterDTO registerDTO)
        {
            var registerUser = await _userRepository.GetByExpression(u => u.Email.ToLower() == registerDTO.Email.ToLower());

            if (registerUser != null)
                return new Response<JWT>(null, true, "user already exists");

            var user = _mapper.Map<User>(registerDTO);

            user.RoleId = 3;
            await _userRepository.Add(user);

            var role = await _roleService.GetRoleById(user.RoleId);

            var token = CreateToken(user,role.Name);

            var outGoingEmail = new EmailDTO
            {
                RecieverEmail = user.Email,
                RecieverName = user.Firstname,
                Subject = "Welcome to CryptoHub",
                plainTextContent = "Enjoy, using our website",
            };

            _sendInBlueEmailService.Sendemail(outGoingEmail);

            return new Response<JWT>(token, false, "registered");
        }

        public async Task<UserDTO> ForgotPassword(string email)
        {
            var user = await _userRepository.GetByExpression(u => u.Email.ToLower() == email.ToLower());
            if (user == null)
                return null;

            user.OTP = new Random().Next(1000, 9999);
            user.OTPExpirationTime = DateTime.UtcNow.AddMinutes(30);
            user.HasForgottenPassword = true;

            var outGoingEmail = new EmailDTO
            {
                RecieverEmail = user.Email,
                RecieverName = user.Firstname,
                Subject = "One Time Pin",
                plainTextContent = "Your OTP is " + user.OTP,
                htmlContent = string.Empty,
            };

            await _userRepository.Update(user);

            _sendInBlueEmailService.Sendemail(outGoingEmail);

            return _mapper.Map<UserDTO>(user);   
        }

        public async Task<Response<UserDTO>> ValidateOTP(string email, int OTP)
        {
            var user =  await _userRepository.GetByExpression(u => u.Email.ToLower() == email.ToLower());
            if (user == null)
                return new Response<UserDTO>(null, true, "user not found");

            if(!user.HasForgottenPassword.Value)
                return new Response<UserDTO>(null, true, "forgot password not requested");

            if (user.OTPExpirationTime < DateTime.UtcNow)
                return new Response<UserDTO>(null, true, "OTP expired");

            if (user.OTP != OTP)
                return new Response<UserDTO>(null, true, "OTP invalid");

            var userDTO = _mapper.Map<UserDTO>(user);

            return new Response<UserDTO>(userDTO, false, "otp confirmed");

        }

        public async Task<Response<UserDTO>> UpdateForgotPassword(string email, string password)
        {
            var user = await _userRepository.GetByExpression(u => u.Email.ToLower() == email.ToLower());
            if (user == null)
                return new Response<UserDTO>(null, true, "user not found");

            if (!user.HasForgottenPassword.Value)
                return new Response<UserDTO>(null, true, "forgot password not requested");

            user.Password = password;
            user.HasForgottenPassword = false;

            await _userRepository.Update(user);

            var userDTO = _mapper.Map<UserDTO>(user);
            return new Response<UserDTO>(userDTO, false, "password reset");

        }



        private JWT CreateToken(User user, string userRole)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", user.UserId.ToString()),
                new Claim("username", user.Username),
                new Claim("firstname", user.Firstname),
                new Claim("lastname", user.Lastname),
                new Claim("roles",userRole)
                 
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), 
                signingCredentials: cred);

            var JwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new JWT(JwtToken);
        }



    }
}

using Domain.Models;
using Infrastructure.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.AuthorizationService
{
    public interface IAuthorizationService
    {
        Task<Response<JWT>> Login(LoginDTO loginDTO);

        Task<Response<JWT>> Register(RegisterDTO registerDTO);

        Task<UserDTO> ForgotPassword(string email);

        Task<Response<UserDTO>> ValidateOTP(string email, int OTP);

        Task<Response<UserDTO>> UpdateForgotPassword(string email, string password);
        
    }
}

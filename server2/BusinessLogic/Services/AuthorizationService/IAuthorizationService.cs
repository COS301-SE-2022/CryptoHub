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
    }
}

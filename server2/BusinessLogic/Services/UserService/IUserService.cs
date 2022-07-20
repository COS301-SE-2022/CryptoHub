using Domain.Models;
using Infrastructure.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsers();

        Task<UserDTO> GetById(int id);


        Task<UserDTO> GetUserByEmail(string email);


        Task<UserDTO> AddUser(User user);


        Task<UserDTO> UpateUser(User user);


        Task Delete(int id);

        Task<List<SearchDTO>> SuggestedUsers(int id);
    }
}

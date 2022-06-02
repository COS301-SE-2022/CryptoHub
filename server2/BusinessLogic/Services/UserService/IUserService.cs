using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();

        Task<User?> GetById(int id);


        Task<User?> GetUserByEmail(string email);


        Task<User?> AddUser(User user);


        Task<User?> UpateUser(User user);


        Task Delete(int id);
        

    }
}

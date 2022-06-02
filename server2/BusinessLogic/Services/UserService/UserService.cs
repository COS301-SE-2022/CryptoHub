using Domain.IRepository;
using Domain.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User?> GetById(int id)
        {
            var response = await _userRepository.GetById(u => u.UserId == id);
            if (response == null)
                return null;

            return response;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var response = await _userRepository.FindOne(u => u.Email == email);
            if (response == null)
                return null;

            return response;
        }

        public async Task<User?> AddUser(User user)
        {
            var response = await _userRepository.FindOne(u => u.Email == user.Email);
            if (response == null)
                return null;

            await _userRepository.Add(user);

            return user;

        }

        public async Task<User?> UpateUser(User user)
        {
            var response = await _userRepository.Update(u => u.UserId == user.UserId, user);
            if (response == null)
                return null;

            return response;
        }

        public async Task Delete(int id)
        {
            await _userRepository.DeleteOne(u => u.UserId == id);
        }

    }
}

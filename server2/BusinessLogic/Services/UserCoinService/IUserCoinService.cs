using Domain.Models;
using Infrastructure.DTO.UserCoinDTOs;
using Infrastructure.DTO.CoinDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.UserCoinService
{
    public interface IUserCoinService
    {
        Task<List<UserCoinDTO>> GetAllUserCoins();

        Task<List<UserCoinDTO>> GetUserCoins(int id);

        Task<List<UserCoinDTO>> GetCoinFollowers(string coinName);

        Task<object> GetCoinFollowCount(string coinName);
       
        Task<Response<string>> FollowCoin(int userId, string coinName);

        Task<Response<string>> UnfollowCoin(int userId, string coinName);
        
        Task<List<CoinDTO>> GetCoinsFollowedByUser(int userId);
        
    }
}

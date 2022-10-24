using Domain.Models;
using Infrastructure.DTO.CoinDTOs;
using Infrastructure.DTO.ImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.CoinService
{
    public interface ICoinService
    {
        Task<CoinDTO> GetCoin(int id);
        Task<CoinDTO> GetCoinByName(string name);
        Task<List<CoinDTO>> GetAllCoins();
        Task<CoinDTO> UpdateCoin(CoinDTO coin);
        Task<CoinDTO> AddCoin(CoinDTO coin);
        Task<Response<Coin>> UpdateCoinProfileImage(int coinId, CreateImageDTO createImageDTO);
        Task<Response<object>> GetCoinRating(string name);
        Task<Response<object>> GetCoinRatingByUserId(int userId, string CoinName);

        Task<Response<object>> GetCoinSentiment(string coinTag);
    }
}

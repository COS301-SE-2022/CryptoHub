using AutoMapper;
using BusinessLogic.Services.ImageService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.CoinDTOs;
using Infrastructure.DTO.ImageDTOs;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.CoinService
{
    public class CoinService : ICoinService
    {
        private readonly ICoinRepository _coinRepository;
        private readonly IImageService _imageService;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICoinRatingRepository _coinRatingRepository;
        private readonly IMapper _mapper;
        public CoinService(ICoinRepository coinRepository, IImageService imageService, IMapper mapper, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ICoinRatingRepository coinRatingRepository)
        {
            _coinRepository = coinRepository;
            _imageService = imageService;
            _mapper = mapper;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _coinRatingRepository = coinRatingRepository;
        }

        public async Task<CoinDTO> GetCoin(int id)
        {
            var coin = await _coinRepository.GetById(c => c.CoinId == id);
            return _mapper.Map<CoinDTO>(coin);
        }

        public async Task<CoinDTO> GetCoinByName(string name)
        {
            var coin = await _coinRepository.FindOne(c => c.CoinName == name);
            return _mapper.Map<CoinDTO>(coin);
        }

        public async Task<List<CoinDTO>> GetAllCoins()
        {
            var coins = await _coinRepository.GetAll();
            return _mapper.Map<List<CoinDTO>>(coins);
        }

        public async Task<Response<Coin>> UpdateCoinProfileImage(int coinId, CreateImageDTO createImageDTO)
        {
            var coin = await _coinRepository.GetByExpression(c => c.CoinId == coinId);

            if (coin == null)
                return new Response<Coin>(null, true, "Coin not found");

            createImageDTO.Name = $"coin-{coin.CoinId}";

            var response = await _imageService.AddImage(createImageDTO);

            if (response.HasError)
                return new Response<Coin>(null, true, response.Message);

            coin.ImageId = response.Model.ImageId;
            coin.ImageUrl = response.Model.Url;

            await _coinRepository.Update(coin);

            return new Response<Coin>(coin, false, "coin updated");

        }

        public async Task<CoinDTO> UpdateCoin(CoinDTO coin)
        {
            var newCoin = new Coin
            {
                CoinId = coin.CoinId,
                CoinName = coin.CoinName,
                //ImageId = coin.ImageId
            };
            var response = await _coinRepository.Update(c => c.CoinId == newCoin.CoinId, newCoin);
            if (response == null)
                return null;

            return _mapper.Map<CoinDTO>(coin);
        }

        public async Task<Response<object>> GetCoinRating(string name)
        {
           
            var coin = await  _coinRepository.GetByExpression(c => c.CoinName == name);
            if (coin == null)
                return new Response<object>(null, true, "coin not found");

            var coinRating = await _coinRatingRepository.ListByExpression(c => c.CoinId == coin.CoinId);

            if(coinRating.Count()<1)
                return new Response<object>(null, false, "");

            var response = new
            {
                Rating = Math.Round(coinRating.Average(c => c.Rating), 2),
                Count = coinRating.Count()
            };

            return new Response<object>(response, false, "");

        }
    }
}

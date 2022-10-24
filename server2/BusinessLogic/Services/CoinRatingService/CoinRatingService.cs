using BusinessLogic.Services.CoinService;
using BusinessLogic.Services.UserService;
using Domain.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.CoinRatingService
{
    public class CoinRatingService : ICoinRatingService
    {

        private readonly ICoinRatingRepository _coinRatingRepository;
        private readonly IUserService _userService;
        private readonly ICoinService _coinService;

        public CoinRatingService(ICoinRatingRepository coinRatingRepository, IUserService userService,
            ICoinService coinService)
        {
            _coinRatingRepository = coinRatingRepository;
            _userService = userService;
            _coinService = coinService;
        }

        public async Task<Response<string>> RateCoin(int userId, string coinName, int rating)
        {
            var tempCoin = await _coinService.GetCoinByName(coinName);
            var user = await _userService.GetById(userId);

            if (user == null)
                return new Response<string>(null, true, "user not found");

            var coin = await _coinService.GetCoin(tempCoin.CoinId);

            if (coin == null)
                return new Response<string>(null, true, "coin not found");

            if (rating < 0 || rating > 6)
                return new Response<string>(null, true, "rating must be between 1 and 5");


            var coinRating = await _coinRatingRepository.FindOne(cr => cr.UserId == user.UserId && cr.CoinId == coin.CoinId);

            if (coinRating == null)
            {
                coinRating = new CoinRating
                {
                    UserId = user.UserId,
                    CoinId = coin.CoinId,
                };
            }

            coinRating.Rating = rating;

            await _coinRatingRepository.Update(coinRating);

            return new Response<string>(null, false, "Coin has been rated");

        }
    }
}

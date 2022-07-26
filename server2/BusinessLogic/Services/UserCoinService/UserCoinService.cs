using AutoMapper;
using BusinessLogic.Services.CoinService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.UserCoinDTOs;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Services.UserCoinService
{
    public class UserCoinService : IUserCoinService
    {
        private readonly ICoinService _coinService;
        private readonly IUserCoinRepository _userCoinRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICoinRepository _coinRepository;
        private readonly IMapper _mapper;
        public UserCoinService(ICoinService coinService, IUserCoinRepository userCoinRepository,
            IUserRepository userRepository, ICoinRepository coinRepository,
            IMapper mapper)
        {
            _coinService = coinService;
            _userCoinRepository = userCoinRepository;
            _userRepository = userRepository;
            _coinRepository = coinRepository;
            _mapper = mapper;
        }

        public async Task<List<UserCoinDTO>> GetAllUserCoins()
        {
            var coins = await _userCoinRepository.GetAll();
            return _mapper.Map<List<UserCoinDTO>>(coins);
        }

        public async Task<List<UserCoinDTO>> GetUserCoins(int id)
        {
            var userCoin = await _userCoinRepository.ListByExpression(u => u.UserId == id);
            var coins = await _coinRepository.GetAll();

            var usercoins = from uc in userCoin
                            join c in coins
                            on uc.CoinId equals c.CoinId
                            select new UserCoinDTO
                            {
                                UserId = uc.UserId,
                                CoinId = uc.CoinId,
                            };

            return _mapper.Map<List<UserCoinDTO>>(usercoins);
        }

        public async Task<List<UserCoinDTO>> GetCoinFollowers(string coinName)
        {
            var coin = await _coinService.GetCoinByName(coinName);

            var userCoin = await _userCoinRepository.ListByExpression(u => u.CoinId == coin.CoinId);
            var users = await _userRepository.GetAll();

            var usercoins = from uc in userCoin
                            join u in users
                            on uc.UserId equals u.UserId
                            select new UserCoinDTO
                            {
                                UserId = uc.UserId,
                                CoinId = uc.CoinId,
                            };

            return _mapper.Map<List<UserCoinDTO>>(usercoins);
        }

        public async Task<object> GetCoinFollowCount(int id)
        {
            var userCoin = await _userCoinRepository.ListByExpression(u => u.CoinId == id);
            var users = await _userRepository.GetAll();

            var usercoins = from uc in userCoin
                            join u in users
                            on uc.UserId equals u.UserId
                            select new UserCoinDTO
                            {
                                UserId = uc.UserId,
                                CoinId = uc.CoinId,
                            };

            if (usercoins == null)
                return new Response<object>(null, true, "no Coin follows");

            return new Response<object>(new { Count = usercoins.Count() }, false, "");
        }

        public async Task<Response<string>> FollowCoin(int userId, string coinName)
        {
            //var coin = await _coinService.GetCoin(coinId);
            var coin = await _coinService.GetCoinByName(coinName);

            if (coin == null)
                return new Response<string>(null, true, "Coin does not exist");

            var response = await _userCoinRepository.GetByExpression(uf => uf.UserId == userId && uf.CoinId == coin.CoinId);

            if (response != null)
                return new Response<string>(null, true, "Coin already followed by that user");



            UserCoin userCoin = new UserCoin
            {
                UserId = userId,
                CoinId = coin.CoinId
            };

            await _userCoinRepository.Add(userCoin);
            return new Response<string>(null, false, "Coin has been followed");

        }

        public async Task<Response<string>> UnfollowCoin(int userId, string coinName)
        {
            //var coin = await _coinService.GetCoin(coinId);
            var coin = await _coinService.GetCoinByName(coinName);


            if (coin == null)
                return new Response<string>(null, true, "Coin does not exist");

            var response = await _userCoinRepository.GetByExpression(uf => uf.UserId == userId && uf.CoinId == coin.CoinId);

            if (response == null)
                return new Response<string>(null, true, "Coin not followed by that user");

            await _userCoinRepository.DeleteOne(u => u.UserId == userId && u.CoinId == coin.CoinId);
            return new Response<string>(null, false, "Coin has been unfollowed");

        }
    }
}

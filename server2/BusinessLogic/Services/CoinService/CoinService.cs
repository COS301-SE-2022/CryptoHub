using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.CoinDTOs;
using Infrastructure.Repository;
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
        private readonly IMapper _mapper;
        public CoinService(ICoinRepository coinRepository, IMapper mapper)
        {
            _coinRepository = coinRepository;
            _mapper = mapper;
        }
        public async Task<List<CoinDTO>> GetAllCoins()
        {
            var coins = await _coinRepository.GetAll();
            return _mapper.Map<List<CoinDTO>>(coins);
        }

        public async Task<CoinDTO> UpdateCoin(CoinDTO coin)
        {
            var newCoin = new Coin
            {
                CoinId = coin.CoinId,
                CoinName = coin.CoinName,
                ImageId = coin.ImageId
            };
            var response = await _coinRepository.Update(c => c.CoinId == newCoin.CoinId, newCoin);
            if (response == null)
                return null;

            return _mapper.Map<CoinDTO>(coin);
        }
    }
}

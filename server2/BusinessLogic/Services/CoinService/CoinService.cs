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
        public async Task<List<CoinDTO>> GetAllUsers()
        {
            var coins = await _coinRepository.GetAll();
            return _mapper.Map<List<CoinDTO>>(coins);
        }
    }
}

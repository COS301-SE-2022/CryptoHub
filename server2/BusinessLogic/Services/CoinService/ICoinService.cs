﻿using Domain.Models;
using Infrastructure.DTO.CoinDTOs;
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
        Task<List<CoinDTO>> GetAllCoins();
        Task<CoinDTO> UpdateCoin(CoinDTO coin);
    }
}
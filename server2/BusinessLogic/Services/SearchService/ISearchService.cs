using Domain.Models;
using Infrastructure.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DTO.CoinDTOs;

namespace BusinessLogic.Services.SearchService
{
    public interface ISearchService
    {
        Task<List<SearchDTO>> SearchUser(int id, string searchterm);
        Task<List<CoinSearchDTO>> SearchCoin(int id, string searchterm);
    }
}

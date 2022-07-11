using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.CoinRatingService
{
    public interface ICoinRatingService
    {
        Task<Response<string>> RateCoin(int userId, int coinId, int rating);
    }
}

using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CryptoHubAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoinFollowerController
    {
        private readonly ICoinFollowerRepository _coinFollowerRepository;
        private readonly ICoinRepository _coinRepository;

        public CoinFollowerController()
        {
        }
    }
}


using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CryptoHubAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoinFollowerController : Controller
    {
        private readonly ICoinFollowerRepository _coinFollowerRepository;
        private readonly ICoinRepository _coinRepository;

        public CoinFollowerController(ICoinFollowerRepository coinFollowerRepository, ICoinRepository coinRepository)
        {
            _coinFollowerRepository = coinFollowerRepository;
            _coinRepository = coinRepository;
        }


        [HttpGet]
        // GET: UserFollowerController
        public async Task<ActionResult<List<CoinFollower>>> GetAllCoinFollowers()
        {
            return Ok(await _coinFollowerRepository.GetAll());
        }
    }
}


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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoinUserFollower(int id)
        {
            var followers = await _coinFollowerRepository.FindRange(uf => uf.UserId == id);
            var users = await _coinRepository.GetAll();



            var userfollowers = from f in followers
                                join u in users
                                on f.FollowId equals u.UserId
                                select new
                                {
                                    UserId = u.UserId,
                                    Username = u.Username
                                };

            return Ok(userfollowers);
        }



    }
}


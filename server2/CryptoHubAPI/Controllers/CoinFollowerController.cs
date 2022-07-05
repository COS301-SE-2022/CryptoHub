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
        private readonly IUserRepository _userRepository;

        public CoinFollowerController(ICoinFollowerRepository coinFollowerRepository, ICoinRepository coinRepository, IUserRepository userRepository)
        {
            _coinFollowerRepository = coinFollowerRepository;
            _coinRepository = coinRepository;
            _userRepository = userRepository;

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
            var users = await _userRepository.GetAll();



            var Coinfollowers = from f in followers
                                join u in users
                                on f.FollowId equals u.UserId
                                select new
                                {
                                    UserId = u.UserId,
                                    Username = u.Username
                                };

            return Ok(Coinfollowers);
        }


        [HttpPost("{userid}/{targetid}")]
        public async Task<IActionResult> FollowUser(int userid, int targetid)
        {
            var response = await _coinFollowerRepository.FindOne(uf => uf.UserId == userid && uf.FollowId == targetid);

            if (response != null)
                return BadRequest("Already following this account");

            CoinFollower coinFollower = new CoinFollower
            {
                UserId = userid,
                FollowId = targetid,
                FollowDate = DateTime.Now
            };

            await _coinFollowerRepository.Add(coinFollower);
            return Ok("user followed");


        }



    }
}


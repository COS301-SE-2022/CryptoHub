using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserFollowerController : Controller
    {

        private readonly IUserFollowerRepository userFollowerRepository;
        private readonly IUserRepository userRepository;

        public UserFollowerController(IUserFollowerRepository userFollowerRepository, IUserRepository userRepository)
        {
            this.userFollowerRepository = userFollowerRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        // GET: UserFollowerController
        public async Task<ActionResult<List<UserFollower>>> GetAllUserFollowers()
        {
            return Ok(await userFollowerRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserUserFollower(int id)
        {
            var followers = await userFollowerRepository.FindRange(uf => uf.UserId == id);
            var users = await userRepository.GetAll();



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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserFollowing(int id)
        {
            var followers = await userFollowerRepository.FindRange(uf => uf.FollowId == id);
            var users = await userRepository.GetAll();



            var userfollowers = from f in followers
                                join u in users
                                on f.UserId equals u.UserId
                                select new
                                {
                                    UserId = u.UserId,
                                    Username = u.Username
                                };

            return Ok(userfollowers);
        }

        [HttpPost("{userid}/{targetid}")]
        public async Task<IActionResult> FollowUser(int userid, int targetid)
        {
            var response = await userFollowerRepository.FindOne(uf => uf.UserId==userid && uf.FollowId==targetid);
            
            if(response != null)
                return BadRequest("Already following this account");

            UserFollower userFollower = new UserFollower
            {
                UserId = userid,
                FollowId = targetid,
                FollowDate = DateTime.Now
            };

            await userFollowerRepository.Add(userFollower);
            return Ok("user followed");


        }








    }
}

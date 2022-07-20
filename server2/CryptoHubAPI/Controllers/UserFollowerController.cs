using BusinessLogic.Services.UserFollowerService;
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

        private readonly IUserFollowerService _userFollowerService;

        public UserFollowerController(IUserFollowerService userFollowerService)
        {
            _userFollowerService = userFollowerService;
        }

        [HttpGet]
        // GET: UserFollowerController
        public async Task<ActionResult<List<UserFollower>>> GetAllUserFollowers()
        {
            return Ok(await _userFollowerService.GetAllUserFollowers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserFollower(int id)
        {

            var userfollowers = await _userFollowerService.GetUserUserFollower(id);

            return Ok(userfollowers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserFollowing(int id)
        {
            var userfollowers = await _userFollowerService.GetUserFollowing(id);

            return Ok(userfollowers);
        }

        [HttpPost("{userid}/{targetid}")]
        public async Task<IActionResult> FollowUser(int userid, int targetid)
        {
            var response = await _userFollowerService.FollowUser(userid, targetid);


            return Ok("user followed");


        }

        [HttpPost("{userId}/{followId}")]
        public async Task<IActionResult> UnfollowUser(int userId, int followId)
        {
            var response = await _userFollowerService.UnfollowUser(userId, followId);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }
    }
}

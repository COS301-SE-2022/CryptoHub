using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {

        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IUserFollowerRepository userFollowerRepository)
        {
            _userFollowerRepository = userFollowerRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        // GET: UserController
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(await _userRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var response = await _userRepository.GetById(u => u.UserId == id);
            if (response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpGet("{name}")]
        public async Task<ActionResult<List<User>>> GetUserByName(string name, int id)
        {
            var response = await _userRepository.FindRange(u => u.Username.ToLower().StartsWith(name.ToLower()) || u.Firstname.ToLower().StartsWith(name.ToLower()) || u.Lastname.ToLower().StartsWith(name.ToLower()));
            if (response == null)
                return NotFound();

            var followers = await _userFollowerRepository.FindRange(uf => uf.FollowId == id);
            var users = await _userRepository.GetAll();

            var userfollowers = from f in followers
                                join u in users
                                on f.UserId equals u.UserId
                                select new
                                {
                                    UserId = u.UserId,
                                    Username = u.Username
                                };

            var flist = from r in response
                        join f in userfollowers
                        on r.UserId equals f.UserId
                        select new
                        {
                            UserId = r.UserId,
                            Username = r.Username
                        };

            return Ok(flist);

        }

        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var response = await _userRepository.FindOne(u => u.Email == email);
            if (response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            return Ok(await _userRepository.Add(user));

        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User user)
        {
            var response = await _userRepository.Update(u => u.UserId == user.UserId, user);
            if (response == null)
                return null;

            return Ok(response);
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.DeleteOne(u => u.UserId == id);
            return Ok();
        }








    }
}

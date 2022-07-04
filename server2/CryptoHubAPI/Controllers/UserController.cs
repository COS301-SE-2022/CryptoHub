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

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
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
        public async Task<ActionResult<List<User>>> GetUserByName(string name)
        {
            var response = await _userRepository.FindRange(u => u.Username.ToLower().StartsWith(name.ToLower()) || u.Firstname.ToLower().StartsWith(name.ToLower()) || u.Lastname.ToLower().StartsWith(name.ToLower()));
            if (response == null)
                return NotFound();

            return Ok(response);

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

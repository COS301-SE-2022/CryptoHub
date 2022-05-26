using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorizationController : Controller
    {

        private readonly IUserRepository userRepository;

        public AuthorizationController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Response<User>>> Login([FromBody] User user)
        {
            var loginUser = await userRepository.FindOne(u => u.Email == user.Email);

            if (loginUser == null)
                return BadRequest(new Response<User>
                {
                    HasError = true,
                    Message = "incorrect username or password",
                    Model = null
                });
            if(!(loginUser.Password == user.Password))
            {
                return BadRequest(new Response<User>
                {
                    HasError = true,
                    Message = "incorrect username or password",
                    Model = null
                });
            }
            return Ok(new Response<User>
            {
                HasError = false,
                Message = "logged in",
                Model = loginUser
            });
        }

        [HttpPost]
        public async Task<ActionResult<Response<User>>> Register([FromBody] User user)
        {
            var registerUser = await userRepository.FindOne(u => u.Email == user.Email);

            if (registerUser != null)
                return BadRequest(new Response<User>
                {
                    HasError = true,
                    Message = "user already exists",
                    Model = null
                });
            await userRepository.Add(user);

            return Ok(new Response<User>
            {
                HasError = false,
                Message = "registered",
                Model = registerUser
            });
        }
    }
    public class Response <T>
    {
        public string Message { get; set; }
        public Boolean HasError { get; set; }
        public T Model { get; set; }

        public Response()
        {

        }
    }

}
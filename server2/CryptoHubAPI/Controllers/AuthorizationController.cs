using CryptoHubAPI.Authentication;
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

        private readonly IUserRepository _userRepository;

        public AuthorizationController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Response<User>>> Login([FromBody] User user)
        {

            string token = CreateToken(user);
            var loginUser = await _userRepository.FindOne(u => u.Email == user.Email);

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
            return Ok(new Response<JWT>
            {

                HasError = false,
                Message = "logged in",
                Model = new JWT(token)
                
            });

            
        }

        [HttpPost]
        public async Task<ActionResult<Response<User>>> Register([FromBody] User user)
        {
            var registerUser = await _userRepository.FindOne(u => u.Email == user.Email);

            if (registerUser != null)
                return BadRequest(new Response<User>
                {
                    HasError = true,
                    Message = "user already exists",
                    Model = null
                });
            await _userRepository.Add(user);

            return Ok(new Response<User>
            {
                HasError = false,
                Message = "registered",
                Model = user
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

    private string CreateToken(User user)
    {
        return string.Empty;
    }

}
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
        public async Task<ActionResult<Response<User>> Login([FromBody] User user)
        {
            var response = await userRepository.FindOne(u => u.Email == user.Email);

            if (response == null)
                return BadRequest(new Response<User>
                {
                    HasError = true,
                    Message = "incorrect username or password",
                    Model = null
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
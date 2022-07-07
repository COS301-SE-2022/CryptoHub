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
            var loginUser = await _userRepository.FindOne(u => u.Email == user.Email);

            if (loginUser == null)
                return BadRequest(new Response<User>
                {
                    HasError = true,
                    Message = "incorrect username or password",
                    Model = null
                });
            if (!(loginUser.Password == user.Password))
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

        [HttpPost]
        public async Task<ActionResult<Response<User>>> UpdateForgotPassword([FromBody] User user)
        {
            var userResponse = await _userRepository.FindOne(u => u.Email == user.Email);
            if (userResponse == null)
            {
                return BadRequest(new Response<User>
                {
                    HasError = true,
                    Message = "user does not exist",
                    Model = null
                });
            }
            //if (user.Password == password)
            //{
            //    return BadRequest(new Response<User>
            //    {
            //        HasError = true,
            //        Message = "new password same as old password",
            //        Model = null
            //    });
            //}
            userResponse.Password = user.Password;
            var response = await _userRepository.Update(u => u.UserId == userResponse.UserId, userResponse);
            if (response == null)
            {

                return BadRequest(new Response<User>
                {
                    HasError = true,
                    Message = "user update failed",
                    Model = null
                });
            }

            return Ok(new Response<User>
            {
                HasError = false,
                Message = "password updated",
                Model = user
            });
        }
    }
    public class Response<T>
    {
        public string Message { get; set; }
        public Boolean HasError { get; set; }
        public T Model { get; set; }

        public Response()
        {

        }
    }

}
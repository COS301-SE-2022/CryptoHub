using BusinessLogic.Services.AuthorizationService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorizationController : Controller
    {

        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        public async Task<ActionResult<JWT>> Login([FromBody] LoginDTO loginDTO)
        {
            var response = await _authorizationService.Login(loginDTO);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Model);
        }

        [HttpPost]
        public async Task<ActionResult<JWT>> Register([FromBody] RegisterDTO registerDTO)
        {
            var response = await _authorizationService.Register(registerDTO);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Model);
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
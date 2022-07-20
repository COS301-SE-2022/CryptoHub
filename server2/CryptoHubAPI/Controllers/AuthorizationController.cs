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

        [HttpPost("{email}")]
        public async Task<ActionResult<string>> ForgetPassord(string email)
        {
            var response = await _authorizationService.ForgotPassword(email);
            if (response == null)
                return BadRequest("user not found");

            return Ok("success");
        }

        [HttpPost("{email}/{otp}")]
        public async Task<ActionResult<string>> ValidateOTP(string email,int otp)
        {
            var response = await _authorizationService.ValidateOTP(email, otp);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }


        [HttpPost("{email}/{password}")]
        public async Task<ActionResult<Response<User>>> UpdateForgotPassword(string email, string password)
        {
            var response = await _authorizationService.UpdateForgotPassword(email, password);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);

        }
            
    }

}
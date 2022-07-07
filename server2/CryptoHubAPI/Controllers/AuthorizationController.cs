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
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDTO)
        {
            var response = await _authorizationService.Login(loginDTO);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO registerDTO)
        {
            var response = await _authorizationService.Register(registerDTO);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }
    }

}
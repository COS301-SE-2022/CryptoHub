using BusinessLogic.Services.UserService;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTO.UserCoinDTOs;
using BusinessLogic.Services.UserCoinService;
using Infrastructure.DTO.UserDTOs;
using Infrastructure.DTO.ImageDTOs;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize(Roles = "Admin,Super")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly IUserCoinService _userCoinService;

        public UserController(IUserService userService, IUserCoinService userCoinService)
        {
            _userService = userService;
            _userCoinService = userCoinService;
        }

        [HttpGet]
        // GET: UserController
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var response = await _userService.GetById(id);
            if (response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDTO>> GetUserByEmail(string email)
        {
            var response = await _userService.GetUserByEmail(email);
            if (response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] User user)
        {
           var response = await _userService.AddUser(user);
           if (response == null)
                return BadRequest();

            return Ok(response);

        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> UpdateUser([FromBody] User user)
        {
            var response = await _userService.UpateUser(user);
            if (response == null)
                return null;

            return Ok(response);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }

        /*[HttpGet]
        public async Task<ActionResult<List<UserCoinDTO>>> GetAllUserCoins()
        {
            return Ok(await _userCoinService.GetAllUserCoins());
        }*/

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<UserCoinDTO>>> GetUserCoins(int userId)
        {
            var response = await _userCoinService.GetUserCoins(userId);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfileImage(CreateImageDTO createImageDTO)
        {
            var response = await _userService.UploadProfilePic(createImageDTO);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }
    }
}

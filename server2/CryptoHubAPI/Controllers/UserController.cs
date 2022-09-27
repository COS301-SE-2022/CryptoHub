using BusinessLogic.Services.UserService;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTO.UserCoinDTOs;
using BusinessLogic.Services.UserCoinService;
using BusinessLogic.Services.SearchService;
using Infrastructure.DTO.UserDTOs;
using Infrastructure.DTO.ImageDTOs;
using BusinessLogic.Services.UserFollowerService;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize(Roles = "Admin,Super")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly IUserCoinService _userCoinService;
        private readonly ISearchService _searchService;
        private readonly IUserFollowerService _userFollowService;

        public UserController(IUserService userService, IUserCoinService userCoinService, ISearchService searchService, IUserFollowerService userFollowService)
        {
            _userService = userService;
            _userCoinService = userCoinService;
            _searchService = searchService;
            _userFollowService = userFollowService;
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
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] RegisterDTO registerDTO)
        {
            var response = await _userService.AddUser(registerDTO);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Model);
        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> UpdateUser([FromBody] User user)
        {
            var response = await _userService.UpdateUser(user);
            if (response == null)
                return NotFound();

            return Ok(response);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserCoinDTO>>> GetAllUsersFollowingCoin(int id)

        {
            var response = await _userCoinService.GetUserCoins(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}/{searchterm}")]
        public async Task<ActionResult<List<User>>> SearchUser(int id, string searchterm)
        {
            var response = await _searchService.SearchUser(id, searchterm);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> SuggestedUsers(int id)
        {
            var response = await _userService.SuggestedUsers(id);
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

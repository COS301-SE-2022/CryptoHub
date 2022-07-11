﻿using BusinessLogic.Services.UserService;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTO.UserCoinDTOs;
using BusinessLogic.Services.UserCoinService;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var response = await _userService.GetById(id);
            if (response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var response = await _userService.GetUserByEmail(email);
            if (response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
           var response = await _userService.AddUser(user);
           if (response == null)
                return BadRequest();

            return Ok(response);

        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User user)
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

        [HttpGet]
        public async Task<ActionResult<List<UserCoinDTO>>> GetAllUserCoins()
        {
            return Ok(await _userCoinService.GetAllUserCoins());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserCoinDTO>>> GetAllUsersFollowingCoin(int id)
        {
            var response = await _userCoinService.GetAllUsersFollowingCoin(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}

using BusinessLogic.Services.CoinService;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTO.CoinDTOs;
using BusinessLogic.Services.CoinRatingService;
using Infrastructure.DTO.UserCoinDTOs;
using BusinessLogic.Services.UserCoinService;
using Infrastructure.DTO.ImageDTOs;

namespace CryptoHubAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoinController : Controller
    {

        private readonly ICoinService _coinService;
        private readonly ICoinRatingService _coinRatingService;
        private readonly IUserCoinService _userCoinService;

        public CoinController(ICoinService coinService, ICoinRatingService coinRatingService, IUserCoinService userCoinService)
        {
            _coinService = coinService;
            _coinRatingService = coinRatingService;
            _userCoinService = userCoinService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Coin>>> GetAllCoins()
        {
            return Ok(await _coinService.GetAllCoins());
        }

        [HttpGet("{coinId}")]
        public async Task<ActionResult<CoinDTO>> GetCoin(int coinId)
        {
            var response = await _coinService.GetCoin(coinId);
            if (response == null)
                return NotFound();

            return Ok(response);
        }


        [HttpPut]
        public async Task<ActionResult<Coin>> UpdateCoin([FromBody] CoinDTO coin)
        {
            var response = await _coinService.UpdateCoin(coin);
            if (response == null)
                return null;

            return Ok(response);
        }

        [HttpPost("{userId}/{coinId}/{rating}")]
        public async Task<ActionResult<string>> RateCoin(int userId, int coinId, int rating)
        {
            var response = await _coinRatingService.RateCoin(userId, coinId, rating);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);

        }

        [HttpPost("{userId}/{coinId}")]
        public async Task<IActionResult> FollowCoin(int userId, int coinId)
        {
            var response = await _userCoinService.FollowCoin(userId, coinId);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        [HttpPost("{userId}/{coinId}")]
        public async Task<IActionResult> UnfollowCoin(int userId, int coinId)
        {
            var response = await _userCoinService.UnfollowCoin(userId, coinId);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        [HttpGet("{coinId}")]
        public async Task<ActionResult<List<UserCoinDTO>>> GetCoinsFollowers(int coinId)
        {
            var response = await _userCoinService.GetCoinFollowers(coinId);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("{coinId}")]
        public async Task<IActionResult> UpdateProfilePic(int coinId, CreateImageDTO createdImageDTO)
        {
            var response = await _coinService.UpdateCoinProfileImage(coinId, createdImageDTO);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);

        }
    }
}


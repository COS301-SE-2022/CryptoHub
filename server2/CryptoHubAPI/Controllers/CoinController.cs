using BusinessLogic.Services.CoinService;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTO.CoinDTOs;
using BusinessLogic.Services.CoinRatingService;
using Infrastructure.DTO.UserCoinDTOs;
using BusinessLogic.Services.UserCoinService;
using BusinessLogic.Services.SearchService;
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
        private readonly ISearchService _searchService;

        public CoinController(ICoinService coinService, ICoinRatingService coinRatingService, IUserCoinService userCoinService, ISearchService searchService)
        {
            _coinService = coinService;
            _coinRatingService = coinRatingService;
            _userCoinService = userCoinService;
            _searchService = searchService;
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

        [HttpGet]
        public async Task<ActionResult<List<UserCoinDTO>>> GetAllUserCoins()
        {
            return Ok(await _userCoinService.GetAllUserCoins());
        }

        [HttpPost("{userId}/{coinName}")]
        public async Task<IActionResult> FollowCoin(int userId, string coinName)
        {
            var response = await _userCoinService.FollowCoin(userId, coinName);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        [HttpPost("{userId}/{coinName}")]
        public async Task<IActionResult> UnfollowCoin(int userId, string coinName)
        {
            var response = await _userCoinService.UnfollowCoin(userId, coinName);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        [HttpGet("{coinName}")]
        public async Task<ActionResult<UserCoinDTO>> GetCoinFollowCount(string coinName)
        {
            var response = await _userCoinService.GetCoinFollowCount(coinName);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{coinName}")]
        public async Task<ActionResult<List<UserCoinDTO>>> GetCoinsFollowers(string coinName)
        {
            var response = await _userCoinService.GetCoinFollowers(coinName);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<CoinDTO>>> GetCoinsFollowedByUser(int userId)
        {
            var response = await _userCoinService.GetCoinsFollowedByUser(userId);

            return Ok(response);
        }

        [HttpGet("{id}/{searchterm}")]
        public async Task<ActionResult<List<User>>> SearchCoin(int id, string searchterm)
        {
            var response = await _searchService.SearchCoin(id, searchterm);
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

        [HttpGet("{coinName}")]
        public async Task<IActionResult> GetCoinRating(string coinName)
        {
            var response = await _coinService.GetCoinRating(coinName);
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Model);
        }
    }
}


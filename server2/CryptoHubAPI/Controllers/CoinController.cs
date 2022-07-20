using BusinessLogic.Services.CoinService;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTO.CoinDTOs;
using BusinessLogic.Services.CoinRatingService;
using Infrastructure.DTO.UserCoinDTOs;
using BusinessLogic.Services.UserCoinService;
using BusinessLogic.Services.SearchService;

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

        [HttpGet("{id}/{searchterm}")]
        public async Task<ActionResult<List<User>>> SearchCoin(int id, string searchterm)
        {
            var response = await _searchService.SearchCoin(id, searchterm);
            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}


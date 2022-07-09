using BusinessLogic.Services.CoinService;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTO.CoinDTOs;
using BusinessLogic.Services.CoinRatingService;

namespace CryptoHubAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoinController : Controller
    {

        private readonly ICoinService _coinService;
        private readonly ICoinRatingService _coinRatingService;

        public CoinController(ICoinService coinService, ICoinRatingService coinRatingService)
        {
            _coinService = coinService;
            _coinRatingService = coinRatingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Coin>>> GetAllCoins()
        {
            return Ok(await _coinService.GetAllCoins());
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
}


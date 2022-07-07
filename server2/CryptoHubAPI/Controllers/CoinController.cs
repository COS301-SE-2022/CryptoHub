using BusinessLogic.Services.CoinService;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTO.CoinDTOs;


namespace CryptoHubAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoinController : Controller
    {

        private readonly ICoinService _coinService;

        public CoinController(ICoinService coinService)
        {
            _coinService = coinService;
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
    }
}


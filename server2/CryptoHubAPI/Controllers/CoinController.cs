using System;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{

	[ApiController]
	[Route("api/[controller]/[action]")]
	public class CoinController : Controller
	{

		private readonly ICoinRepository _coinRepository;
		public CoinController(ICoinRepository coinRepository)
		{
			_coinRepository = coinRepository;
		}

		[HttpGet]
		public async Task<ActionResult<List<Coin>>> GetAllCoins()
		{
			return Ok(await _coinRepository.GetAll());
		}


		[HttpPut]
		public async Task<ActionResult<Post>> UpdateCoin([FromBody] Coin _coin)
		{
			var response = await _coinRepository.Update(u => u.CoinId == _coin.CoinId, _coin);
			if (response == null)
				return null;

			return Ok(response);
		}


	}
}


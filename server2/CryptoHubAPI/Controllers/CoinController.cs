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
		// GET: PostController
		public async Task<ActionResult<List<Coin>>> GetAllCoins()
		{
			return Ok(await _coinRepository.GetAll());
		}



	}
}


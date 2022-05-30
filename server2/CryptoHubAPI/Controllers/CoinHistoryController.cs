using System;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{

	[ApiController]
	[Route("api/[controller]/[action]")]
	public class CoinHistoryController : Controller
	{
		private readonly ICoinHistoryRepository _coinHistoryRepository;
		public CoinHistoryController(ICoinHistoryRepository coinHistoryRepository)
		{
			_coinHistoryRepository = coinHistoryRepository;
		}

		[HttpGet]
		public async Task<ActionResult<List<CoinHistory>>> GetAllCoins()
		{
			return Ok(await _coinHistoryRepository.GetAll());
		}

		[HttpPost]
		public async Task<ActionResult<CoinHistory>> AddPost([FromBody] CoinHistory _coinHistory)
		{
			return Ok(await _coinHistoryRepository.Add(_coinHistory));

		}
	}
}


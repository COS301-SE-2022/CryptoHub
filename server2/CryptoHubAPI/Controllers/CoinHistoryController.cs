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
		public async Task<ActionResult<List<CoinHistory>>> GetAllCoinHistory()
		{
			return Ok(await _coinHistoryRepository.GetAll());
		}

		[HttpPost]
		public async Task<ActionResult<CoinHistory>> AddCoinHistory([FromBody] CoinHistory _coinHistory)
		{
			return Ok(await _coinHistoryRepository.Add(_coinHistory));

		}

		[HttpPut]
		public async Task<ActionResult<CoinHistory>> UpdateCoinHistory([FromBody] CoinHistory _coinHistory)
		{
			var response = await _coinHistoryRepository.Update(u => u.HistoryId == _coinHistory.HistoryId   , _coinHistory);
			if (response == null)
				return null;

			return Ok(response);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await _coinHistoryRepository.DeleteOne(u => u.HistoryId == id);
			return Ok();
		}


	}
}


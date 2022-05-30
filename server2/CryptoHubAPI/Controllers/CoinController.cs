using System;
using Domain.IRepository;
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


	}
}


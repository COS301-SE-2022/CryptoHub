using System;
using Domain.Infrastructure;
using Domain.IRepository;
using Domain.Models;

namespace Infrastructure.Repository
{
	public class CoinRepository : BaseRepository<Coin>, ICoinRepository
	{
		public CoinRepository(CryptoHubDBContext dBContext) : base(dBContext)
		{
		}
	}
}


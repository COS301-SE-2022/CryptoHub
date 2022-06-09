using System;

using Domain.IRepository;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
	public class CoinRepository : BaseRepository<Coin>, ICoinRepository
	{
		public CoinRepository(CryptoHubDBContext dBContext) : base(dBContext)
		{
		}
	}
}


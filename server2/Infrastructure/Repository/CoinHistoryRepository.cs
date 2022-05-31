using System;
using Domain.Infrastructure;
using Domain.IRepository;
using Domain.Models;

namespace Infrastructure.Repository
{
	public class CoinHistoryRepository : BaseRepository<CoinHistory>, ICoinHistoryRepository
	{
		public CoinHistoryRepository(CryptoHubDBContext dBContext) : base(dBContext)
		{

		}
	}
}


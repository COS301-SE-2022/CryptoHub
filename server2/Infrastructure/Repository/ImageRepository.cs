using System;
using Domain.Infrastructure;

namespace Infrastructure.Repository
{
	public class ImageRepository : BaseRepository<Image>, IImageRepository
	{
		public ImageRepository(CryptoHubDBContext dBContext) : base(dBContext)
		{

		}
	}
}


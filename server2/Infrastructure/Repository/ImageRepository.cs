using System;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(CryptoHubDBContext dBContext) : base(dBContext)
        {

        }
    }
}


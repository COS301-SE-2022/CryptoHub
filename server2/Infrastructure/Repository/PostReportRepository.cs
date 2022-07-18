using System;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class PostReportRepository :  BaseRepository<PostReport>, IPostReportRepository
    {
        public PostReportRepository(CryptoHubDBContext dBContext) : base(dBContext)
        {
        }
    }
}


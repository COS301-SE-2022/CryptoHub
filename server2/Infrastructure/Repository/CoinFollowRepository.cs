using Domain.IRepository;
using Infrastructure.Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Infrastructure.Repository
{
    public class CoinFollowRepository : BaseRepository<CoinFollower>, ICoinFollowerRepository
    {
        public CoinFollowRepository (CryptoHubDBContext dBContext) : base(dBContext)
        {
        }
    }
}


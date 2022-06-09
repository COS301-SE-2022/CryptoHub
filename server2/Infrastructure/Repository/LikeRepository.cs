
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class LikeRepository : BaseRepository<Like>, ILikeRepository
    {
        public LikeRepository(CryptoHubDBContext dBContext) : base(dBContext)
        {

        }
    }
}

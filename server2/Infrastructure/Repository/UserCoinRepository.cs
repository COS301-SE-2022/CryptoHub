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
    public class UserCoinRepository : BaseRepository<UserCoin>, IUserCoinRepository
    {
        public UserCoinRepository(CryptoHubDBContext dBContext) : base(dBContext)
        {

        }
    }
}


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
    public class UserFollowerRepository : BaseRepository<UserFollower>, IUserFollowerRepository
    {
        public UserFollowerRepository(CryptoHubDBContext dBContext) : base(dBContext)
        {
        }
    }
}

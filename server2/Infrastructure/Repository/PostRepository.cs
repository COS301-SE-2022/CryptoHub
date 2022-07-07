
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
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(CryptoHubDBContext dBContext) : base(dBContext)
        {
        }
    }
}

using Domain.Infrastructure;
using Domain.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(CryptoHubDBContext dBContext) : base(dBContext)
        {

        }
    }
}

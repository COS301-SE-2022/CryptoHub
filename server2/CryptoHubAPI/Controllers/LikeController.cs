using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LikeController : Controller
    {
        private readonly ILikeRepository likeRepository;

        public LikeController(ILikeRepository likeRepository)
        {
            this.likeRepository = likeRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Like>> AddLike([FromBody] Like Like)
        {
            return Ok(await likeRepository.Add(Like));
        }
    }
}

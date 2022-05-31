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

        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> GetLikeByUserId(int id)
        {
            var response = await likeRepository.FindRange(p => p.UserId == id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> GetLikeByPostId(int id)
        {
            var response = await likeRepository.FindRange(p => p.PostId == id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Like>> AddLike([FromBody] Like Like)
        {
            return Ok(await likeRepository.Add(Like));
        }

        [HttpPut]
        public async Task<ActionResult<Like>> UpdateLike([FromBody] Like Like)
        {
            var response = await likeRepository.Update(u => u.LikeId == Like.LikeId, Like);
            if (response == null)
                return null;

            return Ok(response);
        }
    }
}

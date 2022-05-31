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
        private readonly ILikeRepository _likeRepository;

        public LikeController(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> GetLikeByUserId(int id)
        {
            var response = await _likeRepository.FindRange(p => p.UserId == id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> GetLikeByPostId(int id)
        {
            var response = await _likeRepository.FindRange(p => p.PostId == id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Like>> AddLike([FromBody] Like Like)
        {
            return Ok(await _likeRepository.Add(Like));
        }

        [HttpPut]
        public async Task<ActionResult<Like>> UpdateLike([FromBody] Like Like)
        {
            var response = await _likeRepository.Update(u => u.LikeId == Like.LikeId, Like);
            if (response == null)
                return null;

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _likeRepository.DeleteOne(u => u.LikeId == id);
            return Ok();
        }
    }
}

using BusinessLogic.Services.LikeService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.LikeDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LikeController : Controller
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeRepository)
        {
            _likeService = likeRepository;
        }

        /*[HttpGet]
        // GET: LikeController
        public async Task<ActionResult<List<Like>>> GetAllLikes()
        {
            return Ok(await _likeService.GetAllLikes());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Like>>> GetLikeByUserId(int id)
        {
            var response = await _likeService.GetLikeByUserId(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Like>>> GetLikeByPostId(int id)
        {
            var response = await _likeService.GetLikeByPostId(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLikeCountByPostId(int id)
        {
            var response = await _likeService.GetLikeCountByPostId(id);
            if (response.HasError)
                return NotFound(response.Message);

            return Ok(response.Model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<LikeDTO>>> GetLikeByCommentId(int id)
        {
            var response = await _likeService.GetLikeByCommentId(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLikeCountByCommentId(int id)
        {
            var response = await _likeService.GetLikeCountByCommentId(id);
            if (response.HasError)
                return NotFound(response.Message);

            return Ok(response.Model);
        }

       /* [HttpGet("{id}")]
        public async Task<ActionResult<List<Like>>> GetLikeByReplyId(int id)
        {
            var response = await _likeService.GetLikeByReplyId(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLikeCountByReplyId(int id)
        {
            var response = await _likeService.GetLikeCountByReplyId(id);
            if (response.HasError)
                return NotFound(response.Message);

            return Ok(response.Model);
        }*/

        [HttpGet("{userId}/{postId}")]
        public async Task<ActionResult<LikeDTO>> GetLikeBy(int userId, int postId)
        {
            var response = await _likeService.GetLikeBy(userId, postId);
            if (response == null)
                return NotFound(null);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<LikeDTO>> AddLike([FromBody] LikeDTO like)
        {
            
            var likes = await _likeService.AddLike(like);

            if (likes != null)
                return BadRequest();

            return Ok(await _likeService.AddLike(like));
        }

        [HttpPut]
        public async Task<ActionResult<LikeDTO>> UpdateLike([FromBody] Like like)
        {
            var response = await _likeService.UpdateLike(like);
            if (response == null)
                return null;

            return Ok(response);
        }

        [HttpDelete("{userId}/{postId}")]
        public async Task<IActionResult> Delete(int userId,int postId)
        {


            await _likeService.Delete(userId, postId);
            return Ok();
        }
    }
}

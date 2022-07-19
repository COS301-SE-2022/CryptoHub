using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.CommentDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentByUserId(int id)
        {
            var response = await _commentRepository.FindRange(p => p.UserId == id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentByPostId(int id)
        {
            var response = await _commentRepository.FindRange(p => p.PostId == id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentCountByPostId(int id)
        {
            var response = await _commentRepository.FindRange(p => p.PostId == id);
            if (response == null)
                return NotFound();

            return Ok(new {Count = response.Count()});
        }

        

        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment([FromBody] CommentDTO comment)
        {
            return Ok(await _commentRepository.Add(comment));
        }

        [HttpPut]
        public async Task<ActionResult<Comment>> UpdateComment([FromBody] Comment comment)
        {
            var response = await _commentRepository.Update(u => u.CommentId == comment.CommentId, comment);
            if (response == null)
                return null;

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentRepository.DeleteOne(u => u.CommentId == id);
            return Ok();
        }
    }
}

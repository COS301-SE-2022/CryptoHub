using BusinessLogic.Services.CommentService;
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
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentByUserId(int id)
        {
            var response = await _commentService.GetCommentByUserId(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentByPostId(int id)
        {
            var response = await _commentService.GetCommentByPostId(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentCountByPostId(int id)
        {
            var response = await _commentService.GetCommentCountByPostId(id);
            //if (response == null)
            //    return NotFound();

            return Ok(response);
        }



        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment([FromBody] CommentDTO comment)
        {
            var response = await _commentService.AddComment(comment);
            //if (response == null)
            //    return BadRequest();

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<Comment>> UpdateComment([FromBody] Comment comment)
        {
            var response = await _commentService.UpdateComment(comment);
            if (response == null)
                return null;

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentService.Delete(id);
            return Ok();
        }
    }
}

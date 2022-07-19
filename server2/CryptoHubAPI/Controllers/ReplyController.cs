/*using BusinessLogic.Services.ReplyService;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReplyController : Controller
    {

        private readonly IReplyService _replyService;

        public ReplyController(IReplyService replyService)
        {
            _replyService = replyService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Reply>>> GetRepliesByUserId(int userId)
        {
            var user = await _replyService.GetRepliesByUserId(userId);
            if (user == null)
                return BadRequest("user by specified id not found");

            var reply = await _replyService.GetRepliesByUserId(userId);
            return Ok(reply);

        }

        [HttpGet("{commentId}")]
        public async Task<ActionResult<List<Reply>>> GetRepliesByCommentId(int commentId)
        {

            var comment = await _replyService.GetRepliesByCommentId(commentId);
            if (comment == null)
                return BadRequest("comment by specified id not found");

            var reply = await _replyService.GetRepliesByCommentId(commentId);
            return Ok(reply);

        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetRepliesCountByCommentId(int commentId)
        {

            var comment = await _replyService.GetRepliesCountByCommentId(commentId);
            if (comment.HasError)
                return NotFound(comment.Message);

            var reply = await _replyService.GetRepliesCountByCommentId(commentId);
            
            return Ok(reply.Model);

        }

        [HttpPost]
        public async Task<ActionResult<Reply>> AddReply(Reply reply)
        {
            var response = await _replyService.AddReply(reply);
            return Ok(response);
        }

        [HttpPut("{replyId}")]
        public async Task<IActionResult> UpdateReply(int replyId,Reply reply)
        {
            var resposne = await _replyService.UpdateReply(replyId, reply);
            if(resposne == null)
                return BadRequest("reply by specified id not found");

            return Ok(resposne);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int replyId)
        {
            await _replyService.Delete(replyId);
            return Ok();
        }
    }
}
*/
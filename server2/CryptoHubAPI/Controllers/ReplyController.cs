using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReplyController : Controller
    {

        private readonly IReplyRepository _replyRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;

        public ReplyController(IReplyRepository replyRepository, ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _replyRepository = replyRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        [HttpGet("{userid}")]
        public async Task<ActionResult<List<Reply>>> GetRepliesByUserId(int userId)
        {
            var user = await _userRepository.FindOne(u => u.UserId == userId);
            if (user == null)
                return BadRequest("user by specified id not found");

            var reply = await _replyRepository.FindRange(r => r.UserId == userId);
            return Ok(reply);

        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Reply>>> GetRepliesByCommentId(int commentId)
        {
            
            var comment = await _commentRepository.FindOne(u => u.CommentId == commentId);
            if (comment == null)
                return BadRequest("comment by specified id not found");

            var reply = await _replyRepository.FindRange(r => r.CommentId == commentId);
            return Ok(reply);

        }

        [HttpPost]
        public async Task<ActionResult<Reply>> AddReply(Reply reply)
        {
            var response = await _replyRepository.Add(reply);
            return Ok(response);
        }

        [HttpPut("{replyId}")]
        public async Task<IActionResult> UpdateReply(int replyId,Reply reply)
        {
            var resposne = await _replyRepository.Update(r => r.ReplyId == replyId, reply);
            if(resposne == null)
                return BadRequest("reply by specified id not found");

            return Ok(resposne);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int replyId)
        {
            await _replyRepository.DeleteOne(r => r.ReplyId == replyId);
            return Ok();
        }
    }
}

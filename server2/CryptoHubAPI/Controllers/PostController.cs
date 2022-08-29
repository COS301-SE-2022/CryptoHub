using BusinessLogic.Services.PostService;
using BusinessLogic.Services.UserFollowerService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.PostDTO;
using Infrastructure.DTO.ReportPostDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PostController : Controller
    {

        private readonly IPostService _postService;
        private readonly IUserFollowerService _userFollowerService;

        public PostController(IPostService postService, IUserFollowerService userFollowerService)
        {
            _postService = postService;
            _userFollowerService = userFollowerService;
        }

        [HttpGet]
        // GET: PostController
        public async Task<ActionResult<List<PostDTO>>> GetAllPosts()
        {
            return Ok(await _postService.GetAllPosts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPostByUserId(int id)
        {
            var response = await _postService.GetPostByUserId(id);
            if (response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> AddPost([FromBody] CreatePostDTO createPostDTO)
        {

            var response = await _postService.AddPost(createPostDTO);

            return Ok(response);

        }

        [HttpPut]
        public async Task<ActionResult<PostDTO>> UpdatePost([FromBody] Post Post)
        {
            var response = await _postService.UpdatePost(Post);
            if (response == null)
                return null;
            return Ok(response);
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<PostReport>> Report(int postid, int userid)
        {
            var response = await _postService.Report(postid, userid);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetReportCountByPostId(int id)
        {
            var response = await _postService.GetReportCountByPostId(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet]
        // GET: PostController
        public async Task<ActionResult<List<ReportPostDTO>>> GetAllReportedPosts()
        {
            return Ok(await _postService.GetAllReportedPosts());
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDTO>>> GetFeed()
        {
            var response = await _userFollowerService.GetFeed();
            if (response.HasError)
                return BadRequest(response.Message);

            return Ok(response.Model);

        }

        [HttpGet("{tag}/{startDate}/{endDate}")]
        public async Task<ActionResult<List<PostDTO>>> GetPostsByTag(string tag,DateTime startDate,DateTime endDate)
        {
            return await _postService.GetPostByTag(tag, startDate, endDate);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePostSentiment(List<PostSentimentScoreDTO> postSentimentScoreDTOs)
        {
            await _postService.BatchAddSentimentScore(postSentimentScoreDTOs);
            return Ok();
        }

    }
}

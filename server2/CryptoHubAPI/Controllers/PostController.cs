using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PostController : Controller
    {

        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            this._postRepository = postRepository;
        }

        [HttpGet]
        // GET: PostController
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            return Ok(await _postRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostByUserId(int id)
        {
            var response = await _postRepository.FindRange(p => p.UserId == id);
            if(response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost([FromBody] Post Post)
        {
            return Ok( await _postRepository.Add(Post));

        }

        [HttpPut]       
        public async Task<ActionResult<Post>> UpdatePost([FromBody] Post Post)
        {
            var response = await _postRepository.Update(u => u.PostId == Post.PostId,Post);
            if (response == null)
                return null;
            
            return Ok(response);
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _postRepository.DeleteOne(u => u.PostId == id);
            return Ok();
        }

        
    }
}

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

        private readonly IPostRepository postRepository;

        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [HttpGet]
        // GET: PostController
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            return Ok(await postRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostByUserId(int id)
        {
            var response = await postRepository.FindRange(p => p.UserId == id);
            if(response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost([FromBody] Post Post)
        {
            return Ok( await postRepository.Add(Post));

        }

        [HttpPut]       
        public async Task<ActionResult<Post>> UpdatePost([FromBody] Post Post)
        {
            var response = await postRepository.Update(u => u.PostId == Post.PostId,Post);
            if (response == null)
                return null;
            
            return Ok(response);
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await postRepository.DeleteOne(u => u.PostId == id);
            return Ok();
        }

        
    }
}

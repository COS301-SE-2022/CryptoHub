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
        private readonly IImageRepository _imageRepository;

        public PostController(IPostRepository postRepository, IImageRepository imageRepository)
        {
            _postRepository = postRepository;
            _imageRepository = imageRepository;
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
        public async Task<ActionResult<Post>> AddPost([FromBody] CreatePostDTO createPostDTO)
        {

            Post post = new Post();
            if(createPostDTO.imageDTO != null)
            {
                byte[] imageArray = Convert.FromBase64String(createPostDTO.imageDTO.Blob);

                Image image = new Image();
                image.Image1 = imageArray;

                await _imageRepository.Add(image);
                post.ImageId = image.ImageId;

            }

            post.Content = createPostDTO.Post;
            post.UserId = createPostDTO.UserId;

            return Ok( await _postRepository.Add(post));

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

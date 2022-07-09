﻿using BusinessLogic.Services.PostService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.PostDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PostController : Controller
    {

        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }    

        [HttpGet]
            // GET: PostController
            public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            return Ok(await _postService.GetAllPosts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostByUserId(int id)
        {
            var response = await _postService.GetPostByUserId(id);
            if(response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost([FromBody] CreatePostDTO createPostDTO)
        {

            var response = await _postService.AddPost(createPostDTO);

            return Ok(response);

        }

        [HttpPut]       
        public async Task<ActionResult<Post>> UpdatePost([FromBody] Post Post)
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

        
    }
}

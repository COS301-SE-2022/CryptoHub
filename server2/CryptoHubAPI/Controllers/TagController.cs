using BusinessLogic.Services.TagService;
using Domain.Models;
using Infrastructure.DTO.TagDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<ICollection<Tag>> GetTags()
        {
            //return await _tagService.GetTagsByPost(Id);
            return await _tagService.GetTags();
        }

        [HttpGet]
        public async Task<ICollection<TagDTO>> GetTagByPost(int Id)
        {
            //return await _tagService.GetTagsByPost(Id);
            return null;
        }
    }
}

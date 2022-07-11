using BusinessLogic.Services.TagService;
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
        public async Task<ICollection<TagDTO>> GetTagByPost(int Id)
        {
            return await _tagService.GetTagsByPost(Id);
        }
    }
}

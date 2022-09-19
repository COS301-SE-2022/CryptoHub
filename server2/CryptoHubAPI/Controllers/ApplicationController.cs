using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : Controller
    {
        [HttpGet]
        
        public async Task<IActionResult> Get()
        {
            return Ok(new { message = "hello world"});
        }
    }
}

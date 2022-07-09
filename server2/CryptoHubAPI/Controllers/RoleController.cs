
using BusinessLogic.Services.RoleServices;
using Infrastructure.DTO.RoleDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<RoleDTO>>> GetRoles()
        {
            return Ok(await _roleService.GetRoles());
        }
    }
}

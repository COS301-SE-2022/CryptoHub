using Domain.Models;
using Infrastructure.DTO.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.RoleServices
{
    public interface IRoleService
    {
        Task<ICollection<RoleDTO>> GetRoles();

        Task<ICollection<Roles>> GetRoles2();


        Task<RoleDTO> GetRoleById(int id);
        
    }
}

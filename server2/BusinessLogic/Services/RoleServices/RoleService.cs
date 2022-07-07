using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.RoleServices
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<RoleDTO> GetRoles()
        {
            var roles = await _roleRepository.GetAll();
            return _mapper.Map<RoleDTO>(roles);
        }

        public async Task<RoleDTO> GetRoleById(int id)
        {
            var role = await _roleRepository.GetById(r => r.RoleId == id);
            return _mapper.Map<RoleDTO>(role);
        }
    }
}

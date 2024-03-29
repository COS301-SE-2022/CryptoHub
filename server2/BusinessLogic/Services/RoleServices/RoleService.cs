﻿using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.RoleDTOs;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ICollection<RoleDTO>> GetRoles()
        {
            var roles = await _roleRepository.GetAll();


            return _mapper.Map<ICollection<RoleDTO>>(roles);
        }

        public async Task<RoleDTO> GetRoleById(int id)
        {
            var role = await _roleRepository.GetById(r => r.RoleId == id);
            return _mapper.Map<RoleDTO>(role);
        }
    }
}

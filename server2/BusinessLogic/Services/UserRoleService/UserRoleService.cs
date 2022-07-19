using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DTO.RoleDTOs;
using Infrastructure.DTO.UserRoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.UserRoleServices
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        public UserRoleService(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<UserRoleDTO>> GetRoles()
        {
            var userRoles = await _userRoleRepository.GetAll();
            return _mapper.Map<ICollection<UserRoleDTO>>(userRoles);
        }

        public async Task<UserRoleDTO> GetUserRole(int id)
        {
            var role = await _userRoleRepository.FindOne(u => u.UserId == id);
            return _mapper.Map<UserRoleDTO>(role);
        }
    }
}

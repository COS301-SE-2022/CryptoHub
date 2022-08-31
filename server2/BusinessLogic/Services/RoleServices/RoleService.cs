using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.DTO.RoleDTOs;
using Microsoft.EntityFrameworkCore;
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
        private readonly PGSQLCryptoHubDBContext _pgsqlLCryptoHubDBContext;
        private DbSet<Roles> _roleSet;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper, PGSQLCryptoHubDBContext pgsqlLCryptoHubDBContext)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _pgsqlLCryptoHubDBContext = pgsqlLCryptoHubDBContext;
            _roleSet = pgsqlLCryptoHubDBContext.Set<Roles>();
        }
        public async Task<ICollection<RoleDTO>> GetRoles()
        {
            var roles = await _roleSet.ToListAsync();


            return _mapper.Map<ICollection<RoleDTO>>(roles);
        }

        public async Task<RoleDTO> GetRoleById(int id)
        {
            var role = await _roleRepository.GetById(r => r.RoleId == id);
            return _mapper.Map<RoleDTO>(role);
        }

        public async Task<ICollection<Roles>> GetRoles2()
        {
            return await _roleSet.ToListAsync();
        }
    }
}

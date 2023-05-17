using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.RoleDto;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace jts_backend.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;

        public RoleService(JtsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> CreateRole(CreateRoleDto newRole)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            var role = await _context.role
                .Where(r => r.name.ToLower() == newRole.name.ToLower())
                .FirstOrDefaultAsync();
            //check if role is already exist
            if (role != null)
            {
                response.message = "Role with this name is already exist.";
                response.success = false;
                return response;
            }
            var _newRole = new RoleModel();
            _newRole.name = newRole.name;

            _context.role.Add(_newRole);
            await _context.SaveChangesAsync();
            response.data = "Role added successfully.";
            return response;
        }

        public async Task<ServiceResponse<ICollection<RoleModel>>> GetAllRoles()
        {
            ServiceResponse<ICollection<RoleModel>> response =
                new ServiceResponse<ICollection<RoleModel>>();

            var roles = await _context.role.Select(r => r).ToListAsync();
            response.data = roles;
            return response;
        }

        public async Task<ServiceResponse<RoleModel>> GetRole(int role_id)
        {
            ServiceResponse<RoleModel> response = new ServiceResponse<RoleModel>();
            var role = await _context.role.Where(r => r.role_id == role_id).FirstOrDefaultAsync();
            response.data = role;
            return response;
        }
    }
}

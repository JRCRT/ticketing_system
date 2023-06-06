using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
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

        public async Task<ServiceResponse<GetRoleDto>> CreateRole(CreateRoleDto request)
        {
            var response = new ServiceResponse<GetRoleDto>();

            var role = await _context.role.FirstOrDefaultAsync(
                r => r.name.ToLower() == request.name.ToLower()
            );
            //check if role is already exist
            if (role != null)
            {
                response.message = "Role with this name is already exist.";
                response.success = false;
                return response;
            }
            var newRole = new RoleModel() { name = request.name };

            _context.role.Add(newRole);
            await _context.SaveChangesAsync();
            response.data = _mapper.Map<GetRoleDto>(newRole);
            response.message = "Role added successfully.";
            return response;
        }

        public async Task<ServiceResponse<ICollection<RoleModel>>> GetAllRoles()
        {
            var response = new ServiceResponse<ICollection<RoleModel>>();

            var roles = await _context.role.Select(r => r).ToListAsync();
            response.data = roles;
            return response;
        }

        public async Task<ServiceResponse<RoleModel>> GetRole(int role_id)
        {
            var response = new ServiceResponse<RoleModel>();
            var role = await _context.role.FirstOrDefaultAsync(r => r.role_id == role_id);
            response.data = role;
            return response;
        }
    }
}

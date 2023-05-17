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

        public Task<ServiceResponse<ICollection<GetRole>>> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetRole>> GetRole(int role_id)
        {
            ServiceResponse<GetRole> response = new ServiceResponse<GetRole>();
            var role = await _context.role.Where(r => r.role_id == role_id).FirstOrDefaultAsync();
            GetRole _role = _mapper.Map<GetRole>(role);
            response.data = _role;
            return response;
        }
    }
}

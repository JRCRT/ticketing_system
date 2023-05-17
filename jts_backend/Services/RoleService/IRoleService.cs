using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core.GeoJson;
using jts_backend.Dtos.RoleDto;
using jts_backend.Models;

namespace jts_backend.Services.RoleService
{
    public interface IRoleService
    {
        public Task<ServiceResponse<RoleModel>> GetRole(int role_id);
        public Task<ServiceResponse<ICollection<RoleModel>>> GetAllRoles();
    }
}

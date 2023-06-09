using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using jts_backend.Dtos.RoleDto;
using jts_backend.Models;
using jts_backend.Services.RoleService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<ServiceResponse<RoleModel>>> GetAllRoles()
        {
            var roles = await _roleService.GetAllRoles();
            return Ok(roles);
        }

        [HttpPost("GetRole")]
        public async Task<ActionResult<ServiceResponse<RoleModel>>> GetRole(int id)
        {
            var role = await _roleService.GetRole(id);
            return Ok(role);
        }

        [HttpPost("CreateRole")]
        public async Task<ActionResult<ServiceResponse<string>>> CreateRole(CreateRoleDto newRole)
        {
            var _newRole = await _roleService.CreateRole(newRole);
            if (!_newRole.success)
            {
                return BadRequest(_newRole);
            }
            return Ok(_newRole);
        }
    }
}

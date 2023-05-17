using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using jts_backend.Dtos.RoleDto;
using jts_backend.Models;
using jts_backend.Services.RoleService;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetRole>>> GetRole(int id)
        {
            var role = await _roleService.GetRole(id);
            return Ok(role);
        }
    }
}

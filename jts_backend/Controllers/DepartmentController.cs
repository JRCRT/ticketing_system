using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.DepartmentDto;
using jts_backend.Models;
using jts_backend.Services.DepartmentService;
using Microsoft.AspNetCore.Mvc;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetAllDepartments")]
        public async Task<
            ActionResult<ServiceResponse<ICollection<GetDepartmentDto>>>
        > GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartments();
            return Ok(departments);
        }

        [HttpPost("CreateDepartment")]
        public async Task<ActionResult<ServiceResponse<GetDepartmentDto>>> CreateDepartment(
            CreateDepartmentDto request
        )
        {
            var departments = await _departmentService.CreateDepartment(request);
            return Ok(departments);
        }
    }
}

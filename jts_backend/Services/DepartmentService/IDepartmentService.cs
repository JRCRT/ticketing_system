using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.DepartmentDto;
using jts_backend.Models;

namespace jts_backend.Services.DepartmentService
{
    public interface IDepartmentService
    {
        public Task<ServiceResponse<GetDepartmentDto>> CreateDepartment(
            CreateDepartmentDto request
        );
        public Task<ServiceResponse<ICollection<GetDepartmentDto>>> GetAllDepartments();
    }
}

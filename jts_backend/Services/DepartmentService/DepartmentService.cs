using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Context;
using jts_backend.Dtos.DepartmentDto;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly JtsContext _context;

        public DepartmentService(JtsContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<string>> CreateDepartment(CreateDepartmentDto request)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var department = await _context.department
                .Where(d => d.name.ToLower().Equals(request.name.ToLower()))
                .FirstOrDefaultAsync();
            if (department != null)
            {
                response.message = "Department with this name is already exist.";
                response.success = false;
                return response;
            }

            var newDeparment = new DepartmentModel();
            newDeparment.name = request.name;

            _context.department.Add(newDeparment);
            await _context.SaveChangesAsync();
            response.data = "Added successfully.";
            return response;
        }
    }
}

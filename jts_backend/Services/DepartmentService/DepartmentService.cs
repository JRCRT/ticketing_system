using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.DepartmentDto;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(JtsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> CreateDepartment(CreateDepartmentDto request)
        {
            var response = new ServiceResponse<string>();
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

        public async Task<ServiceResponse<ICollection<GetDepartmentDto>>> GetAllDepartments()
        {
            var response = new ServiceResponse<ICollection<GetDepartmentDto>>();
            var departments = await _context.department
                .Select(d => _mapper.Map<GetDepartmentDto>(d))
                .ToListAsync();
            response.data = departments;
            return response;
        }
    }
}

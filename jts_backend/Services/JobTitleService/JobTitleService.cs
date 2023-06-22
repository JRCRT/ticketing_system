using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.JobTitleDto;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services.JobTitleService
{
    public class JobTitleService : IJobTitleService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;

        public JobTitleService(JtsContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetJobTitleDto>> CreateJobTitle(CreateJobTitleDto request)
        {
            var response = new ServiceResponse<GetJobTitleDto>();
            var jobTitle = await _context.jobTitle
                .Where(j => j.name.ToLower().Equals(request.name.ToLower()))
                .FirstOrDefaultAsync();
            if (jobTitle != null)
            {
                response.message = "Job Title with this name is already exist.";
                response.success = false;
                return response;
            }

            var newJobTitle = new JobTitleModel() { name = request.name };

            _context.jobTitle.Add(newJobTitle);
            await _context.SaveChangesAsync();
            response.data = _mapper.Map<GetJobTitleDto>(newJobTitle);
            return response;
        }

        public async Task<ServiceResponse<ICollection<GetJobTitleDto>>> GetAllJobTitles()
        {
            var response = new ServiceResponse<ICollection<GetJobTitleDto>>();
            var jobTitiles = await _context.jobTitle
                .Select(j => _mapper.Map<GetJobTitleDto>(j))
                .ToListAsync();
            response.data = jobTitiles;
            return response;
        }
    }
}

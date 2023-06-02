using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.PriorityDto;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services.PriorityService
{
    public class PriorityService : IPriorityService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;

        public PriorityService(JtsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetPriorityDto>> CreatePriority(CreatePriorityDto request)
        {
            var response = new ServiceResponse<GetPriorityDto>();
            var priority = await _context.priority.FirstOrDefaultAsync(
                p => p.name.ToLower().Equals(request.name.ToLower())
            );
            if (priority != null)
            {
                response.message = "Already exists.";
                response.success = false;
                return response;
            }

            var newPriority = new PriorityModel() { name = request.name };
            _context.priority.Add(newPriority);
            await _context.SaveChangesAsync();
            response.data = _mapper.Map<GetPriorityDto>(newPriority);
            return response;
        }

        public async Task<ServiceResponse<ICollection<GetPriorityDto>>> GetAllPriority()
        {
            var priorities = await _context.priority
                .Select(p => _mapper.Map<GetPriorityDto>(p))
                .ToListAsync();
            var response = new ServiceResponse<ICollection<GetPriorityDto>>() { data = priorities };
            return response;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.StatusDto;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services.StatusService
{
    public class StatusService : IStatusService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;

        public StatusService(JtsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetStatusDto>> CreateStatus(CreateStatusDto request)
        {
            var response = new ServiceResponse<GetStatusDto>();

            var status = await _context.status.FirstOrDefaultAsync(
                r => r.name.ToLower() == request.name.ToLower()
            );
            //check if role is already exist
            if (status != null)
            {
                response.message = "Status with this name is already exist.";
                response.success = false;
                return response;
            }
            var newStatus = new StatusModel() { name = request.name };

            _context.status.Add(newStatus);
            await _context.SaveChangesAsync();
            response.data = _mapper.Map<GetStatusDto>(newStatus);
            response.message = "Status added successfully.";
            return response;
        }

        public async Task<ServiceResponse<ICollection<GetStatusDto>>> GetAllStatus()
        {
            var statuses = await _context.status
                .Select(s => _mapper.Map<GetStatusDto>(s))
                .ToListAsync();
            var response = new ServiceResponse<ICollection<GetStatusDto>>() { data = statuses };
            return response;
        }
    }
}

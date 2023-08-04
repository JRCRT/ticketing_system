using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.ViewDto;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services.ViewService
{
    public class ViewService : IViewService
    {

        private readonly JtsContext _context;
        private readonly IMapper _mapper;
        public ViewService(JtsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetViewDto>> CreateView(CreateViewDto request)
        {
            var response = new ServiceResponse<GetViewDto>();
            var view = await _context.view.FirstOrDefaultAsync(v => v.name.ToLower().Equals(request.name));
            if(request.name.Equals(view?.name)){
                response.message = "View with the same name already exist";
                response.success = false;
            }
            var newView = new ViewModel(){name = request.name};
            _context.view.Add(newView);
            await _context.SaveChangesAsync();
            response.data = _mapper.Map<GetViewDto>(newView);
            response.message = "Added Successfully";
            return response;
        }

        public async Task<ServiceResponse<ICollection<GetViewDto>>> GetAllViews()
        {
            var response = new ServiceResponse<ICollection<GetViewDto>>();
            var views = await _context.view.Select(v => _mapper.Map<GetViewDto>(v)).ToListAsync();
            response.data = views;
            return response;
        }
    }
}
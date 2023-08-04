using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.ViewDto;
using jts_backend.Models;

namespace jts_backend.Services.ViewService
{
    public interface IViewService
    {
        public Task<ServiceResponse<GetViewDto>> CreateView(CreateViewDto request);
        public Task<ServiceResponse<ICollection<GetViewDto>>> GetAllViews();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.PriorityDto;
using jts_backend.Models;

namespace jts_backend.Services.PriorityService
{
    public interface IPriorityService
    {
        Task<ServiceResponse<ICollection<GetPriorityDto>>> GetAllPriority();
        Task<ServiceResponse<GetPriorityDto>> CreatePriority(CreatePriorityDto request);
    }
}

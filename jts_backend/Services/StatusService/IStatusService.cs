using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.StatusDto;
using jts_backend.Models;

namespace jts_backend.Services.StatusService
{
    public interface IStatusService
    {
        Task<ServiceResponse<GetStatusDto>> CreateStatus(CreateStatusDto request);
        Task<ServiceResponse<ICollection<GetStatusDto>>> GetAllStatus();
    }
}

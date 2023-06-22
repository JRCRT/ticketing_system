using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.JobTitleDto;
using jts_backend.Models;

namespace jts_backend.Services.JobTitleService
{
    public interface IJobTitleService
    {
        public Task<ServiceResponse<GetJobTitleDto>> CreateJobTitle(CreateJobTitleDto request);
        public Task<ServiceResponse<ICollection<GetJobTitleDto>>> GetAllJobTitles();
    }
}

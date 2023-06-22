using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.JobTitleDto;
using jts_backend.Models;
using jts_backend.Services.JobTitleService;
using Microsoft.AspNetCore.Mvc;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobTitleController : ControllerBase
    {
        private readonly IJobTitleService _jobTitleService;

        public JobTitleController(IJobTitleService jobTitleService)
        {
            _jobTitleService = jobTitleService;
        }

        [HttpGet("GetAllJobTitles")]
        public async Task<
            ActionResult<ServiceResponse<ICollection<GetJobTitleDto>>>
        > GetAllDepartments()
        {
            var jobTitles = await _jobTitleService.GetAllJobTitles();
            return Ok(jobTitles);
        }

        [HttpPost("CreateJobTitle")]
        public async Task<ActionResult<ServiceResponse<GetJobTitleDto>>> CreateDepartment(
            CreateJobTitleDto request
        )
        {
            var jobTitle = await _jobTitleService.CreateJobTitle(request);
            return Ok(jobTitle);
        }
    }
}

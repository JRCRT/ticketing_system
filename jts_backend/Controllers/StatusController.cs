using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.StatusDto;
using jts_backend.Models;
using jts_backend.Services.StatusService;
using Microsoft.AspNetCore.Mvc;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet("GetAllStatus")]
        public async Task<ActionResult<ServiceResponse<Collection<GetStatusDto>>>> GetAllStatus()
        {
            var statuses = await _statusService.GetAllStatus();
            return Ok(statuses);
        }

        [HttpPost("CreateStatus")]
        public async Task<ActionResult<ServiceResponse<GetStatusDto>>> CreateStatus(
            CreateStatusDto request
        )
        {
            var status = await _statusService.CreateStatus(request);
            return Ok(status);
        }
    }
}

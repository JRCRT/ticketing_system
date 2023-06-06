using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.PriorityDto;
using jts_backend.Models;
using jts_backend.Services.PriorityService;
using Microsoft.AspNetCore.Mvc;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityService _priorityService;

        public PriorityController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        [HttpGet("GetAllPriorities")]
        public async Task<
            ActionResult<ServiceResponse<ICollection<GetPriorityDto>>>
        > GetAllPriorities()
        {
            var response = await _priorityService.GetAllPriority();
            return Ok(response);
        }

        [HttpPost("CreatePriority")]
        public async Task<ActionResult<ServiceResponse<GetPriorityDto>>> CreatePriority(
            CreatePriorityDto request
        )
        {
            var response = await _priorityService.CreatePriority(request);
            return Ok(response);
        }
    }
}

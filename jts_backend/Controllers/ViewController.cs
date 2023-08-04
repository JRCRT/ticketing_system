using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.ViewDto;
using jts_backend.Models;
using jts_backend.Services.ViewService;
using Microsoft.AspNetCore.Mvc;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewController : ControllerBase
    {
        private readonly IViewService _viewService;
        public ViewController(IViewService viewService)
        {
            _viewService = viewService;
        }
        
        [HttpGet("GetAllViews")]
        public async Task<ActionResult<ServiceResponse<ICollection<GetViewDto>>>> GetAllViews(){
            var response = await _viewService.GetAllViews();
            return Ok(response);
        } 

        [HttpPost("CreateView")]
        public async Task<ActionResult<ServiceResponse<GetViewDto>>> CreateView(CreateViewDto request){
            var response = await _viewService.CreateView(request);
            return Ok(response);
        }
    }
}
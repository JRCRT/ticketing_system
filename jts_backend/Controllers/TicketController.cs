using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.TicketDto;
using jts_backend.Models;
using jts_backend.Services.TicketService;
using Microsoft.AspNetCore.Mvc;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("CreateTicket")]
        public async Task<ActionResult<ServiceResponse<GetTicketDto>>> CreateTicket(
            CreateTicketDto request
        )
        {
            var response = await _ticketService.CreateTicket(request);
            return Ok(response);
        }

        [HttpGet("GetAllTickets")]
        public async Task<ActionResult<ServiceResponse<ICollection<GetTicketDto>>>> GetAllTickets()
        {
            var response = await _ticketService.GetAllTickets();
            return Ok(response);
        }
    }
}

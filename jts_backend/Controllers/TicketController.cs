using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.TicketDto;
using jts_backend.Models;
using jts_backend.Services.TicketService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using jts_backend.Dtos.SignatoryDto;

namespace jts_backend.Controllers
{
    // [Authorize]
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
            [FromForm] CreateTicketDto request
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

        [HttpPost("GetAllTicketByStatus")]
        public async Task<
            ActionResult<ServiceResponse<ICollection<GetTicketDto>>>
        > GetAllTicketByStatus(string status)
        {
            var response = await _ticketService.GetTicketByStatus(status);
            return Ok(response);
        }

        [HttpGet("GetAllTicketsToday")]
        public async Task<ActionResult<ICollection<GetTicketDto>>> GetAllTicketsToday(int userId)
        {
            var response = await _ticketService.GetTodayTickets(userId);
            return Ok(response);
        }

        [HttpPost("GetTicketById")]
        public async Task<ActionResult<GetTicketDto>> GetTicketById(int id)
        {
            var response = await _ticketService.GetTicketById(id);
            return Ok(response);
        }

        [HttpPost("GetTicketByUser")]
        public async Task<ActionResult<GetTicketDto>> GetTicketByUser(TicketByUserDto request)
        {
            var response = await _ticketService.GetTicketByUser(request);
            return Ok(response);
        }

        [HttpPost("GetTicketsForApproval")]
        public async Task<ActionResult<GetTicketDto>> GetTicketsForApproval(TicketByUserDto request)
        {
            var response = await _ticketService.GetTicketsForApproval(request);
            return Ok(response);
        }

        [HttpPost("ApproveTicket")]
        public async Task<ActionResult<GetTicketDto>> ApproveTicket(ApproveTicketDto request)
        {
            var response = await _ticketService.ApproveTicket(request);
            return Ok(response);
        }

        [HttpPost("DeclineTicket")]
        public async Task<ActionResult<GetTicketDto>> DeclineTicket(DeclineTicketDto request)
        {
            var response = await _ticketService.DeclineTicket(request);
            return Ok(response);
        }
    }
}

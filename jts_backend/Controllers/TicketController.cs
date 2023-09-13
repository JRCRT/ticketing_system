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
        public async Task<ActionResult<ServiceResponse<GetTicketsDto>>> GetAllTickets()
        {
            var response = await _ticketService.GetAllTickets();
            return Ok(response);
        }

        [HttpPost("GetAllTicketByStatus")]
        public async Task<ActionResult<ServiceResponse<GetTicketsDto>>> GetAllTicketByStatus(
            TicketByStatusDto request
        )
        {
            var response = await _ticketService.GetTicketByStatus(request);
            return Ok(response);
        }

        [HttpPost("GetAllTicketsToday")]
        public async Task<ActionResult<ServiceResponse<GetTicketsDto>>> GetAllTicketsToday(
            TicketsTodayDto request
        )
        {
            var response = await _ticketService.GetTodayTickets(request);
            return Ok(response);
        }

        [HttpPost("GetTicketById")]
        public async Task<ActionResult<ServiceResponse<GetTicketDto>>> GetTicketById(int id)
        {
            var response = await _ticketService.GetTicketById(id);
            return Ok(response);
        }

        [HttpPost("GetTicketByUser")]
        public async Task<ActionResult<ServiceResponse<GetTicketsDto>>> GetTicketByUser(
            TicketByUserDto request
        )
        {
            var response = await _ticketService.GetTicketByUser(request);
            return Ok(response);
        }

        [HttpPost("GetTicketsForApproval")]
        public async Task<ActionResult<ServiceResponse<GetTicketsDto>>> GetTicketsForApproval(
            TicketByUserDto request
        )
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

        [HttpPost("RejectTicket")]
        public async Task<ActionResult<GetTicketDto>> RejectTicket(RejectTicket request)
        {
            var response = await _ticketService.RejectTicket(request);
            return Ok(response);
        }

        [HttpPost("DoneTicket")]
        public async Task<ActionResult<GetTicketDto>> DoneTicket(DoneTicketDto request)
        {
            var response = await _ticketService.DoneTicket(request);
            return Ok(response);
        }
    }
}

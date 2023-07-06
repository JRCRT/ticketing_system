using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.TicketDto;
using jts_backend.Models;

namespace jts_backend.Services.TicketService
{
    public interface ITicketService
    {
        Task<ServiceResponse<GetTicketDto>> CreateTicket(
            CreateTicketDto request,
            IFormFileCollection file
        );
        Task<ServiceResponse<ICollection<GetTicketDto>>> GetAllTickets();
        Task<ServiceResponse<ICollection<GetTicketDto>>> GetTicketByStatus(string status);
        Task<ServiceResponse<ICollection<GetTicketDto>>> GetTodayTickets();
    }
}

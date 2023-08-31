using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.SignatoryDto;
using jts_backend.Dtos.TicketDto;
using jts_backend.Models;

namespace jts_backend.Services.TicketService
{
    public interface ITicketService
    {
        Task<ServiceResponse<GetTicketDto>> CreateTicket(CreateTicketDto request);
        Task<ServiceResponse<ICollection<GetTicketDto>>> GetAllTickets();
        Task<ServiceResponse<ICollection<GetTicketDto>>> GetTicketByStatus(string status);
        Task<ServiceResponse<ICollection<GetTicketDto>>> GetTodayTickets(int userId);
        Task<ServiceResponse<ICollection<GetTicketDto>>> GetTicketByUser(TicketByUserDto request);
        Task<ServiceResponse<GetTicketDto>> GetTicketById(int id);
        Task<ServiceResponse<ICollection<GetTicketDto>>> GetTicketsForApproval(
            TicketByUserDto request
        );
        Task<ServiceResponse<GetTicketDto>> ApproveTicket(ApproveTicketDto signatory);
        Task<ServiceResponse<GetTicketDto>> RejectTicket(RejectTicket signatory);
        Task<ServiceResponse<GetTicketDto>> DoneTicket(DoneTicketDto request);
    }
}

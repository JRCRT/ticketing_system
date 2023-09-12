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
        Task<ServiceResponse<GetTicketsDto>> GetAllTickets();
        Task<ServiceResponse<GetTicketsDto>> GetTicketByStatus(string status);
        Task<ServiceResponse<GetTicketsDto>> GetTodayTickets(int userId);
        Task<ServiceResponse<GetTicketsDto>> GetTicketByUser(TicketByUserDto request);
        Task<ServiceResponse<GetTicketDto>> GetTicketById(int id);
        Task<ServiceResponse<GetTicketsDto>> GetTicketsForApproval(TicketByUserDto request);
        Task<ServiceResponse<GetTicketDto>> ApproveTicket(ApproveTicketDto signatory);
        Task<ServiceResponse<GetTicketDto>> RejectTicket(RejectTicket signatory);
        Task<ServiceResponse<GetTicketDto>> DoneTicket(DoneTicketDto request);
    }
}

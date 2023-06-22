using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using jts_backend.Context;
using jts_backend.Dtos.TicketDto;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services.TicketService
{
    public class TicketService : ITicketService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;

        public TicketService(JtsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetTicketDto>> CreateTicket(CreateTicketDto request)
        {
            var response = new ServiceResponse<GetTicketDto>();
            var preparedBy = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
                .FirstOrDefaultAsync(u => u.user_id == request.user_id);
            var priority = await _context.priority.FirstOrDefaultAsync(
                p => p.priority_id == request.priority_id
            );
            var status = await _context.status.FirstOrDefaultAsync(
                s => s.status_id == request.status_id
            );

            if (preparedBy == null || priority == null || status == null)
            {
                response.success = false;
                return response;
            }

            var ticket = new TicketModel()
            {
                background = request.background,
                content = request.content,
                declined_reason = request.declined_reason,
                reason = request.reason,
                subject = request.subject,
                condition = request.condition,
                priority = priority,
                status = status,
                user = preparedBy,
                date_created = request.date_created.Date
            };

            _context.ticket.Add(ticket);
            await _context.SaveChangesAsync();

            var signatories = request.signatories;
            var _signatories = new Collection<GetUserDto>();
            foreach (var signatory in signatories)
            {
                var user = await _context.user
                    .Include(u => u.role)
                    .Include(u => u.department)
                    .FirstOrDefaultAsync(u => u.user_id == signatory.user_id);
                var approver = new SignatoryModel()
                {
                    ticket = ticket,
                    user = user,
                    type = signatory.type
                };

                await _context.approver.AddAsync(approver);
                await _context.SaveChangesAsync();

                if (user == null)
                {
                    response.success = false;
                    response.message = "Something went wrong.";
                    return response;
                }
                _signatories.Add(_mapper.Map<GetUserDto>(user));
            }

            var responseData = new GetTicketDto()
            {
                ticket = _mapper.Map<TicketDto>(ticket),
                signatories = _signatories
            };
            response.data = responseData;
            response.message = "Ticket successfully created.";

            return response;
        }

        public async Task<ServiceResponse<ICollection<GetTicketDto>>> GetAllTickets()
        {
            var response = new ServiceResponse<ICollection<GetTicketDto>>();
            var responseData = new Collection<GetTicketDto>();
            var tickets = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.user)
                .Include(u => u.user.role)
                .Include(u => u.user.department)
                .Select(t => t)
                .ToListAsync();

            foreach (var ticket in tickets)
            {
                var approvers = new Collection<GetUserDto>();
                var signatories = await _context.approver
                    .Include(a => a.user)
                    .Include(a => a.ticket)
                    .Where(a => a.ticket!.ticket_id == ticket.ticket_id)
                    .Select(a => a)
                    .ToListAsync();
                /* var _signatories = signatories.Where(s => s.ticket!.ticket_id == ticket.ticket_id); */
                foreach (var signatory in signatories)
                {
                    var user = await _context.user
                        .Include(u => u.role)
                        .Include(u => u.department)
                        .FirstOrDefaultAsync(u => u.user_id == signatory.user!.user_id);
                    approvers.Add(_mapper.Map<GetUserDto>(user!));
                }

                var data = new GetTicketDto()
                {
                    ticket = _mapper.Map<TicketDto>(ticket),
                    signatories = approvers
                };

                responseData.Add(data);
            }

            response.data = responseData;

            return response;
        }

        public async Task<ServiceResponse<ICollection<GetTicketDto>>> GetTicketByStatus(
            string status
        )
        {
            var response = new ServiceResponse<ICollection<GetTicketDto>>();
            var responseData = new Collection<GetTicketDto>();
            var tickets = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.user)
                .Include(u => u.user.role)
                .Include(u => u.user.department)
                .Where(t => t.status.name.Equals(status))
                .Select(t => t)
                .ToListAsync();

            foreach (var ticket in tickets)
            {
                var approvers = new Collection<GetUserDto>();
                var signatories = await _context.approver
                    .Include(a => a.user)
                    .Include(a => a.ticket)
                    .Where(a => a.ticket!.ticket_id == ticket.ticket_id)
                    .Select(a => a)
                    .ToListAsync();
                /* var _signatories = signatories.Where(s => s.ticket!.ticket_id == ticket.ticket_id); */
                foreach (var signatory in signatories)
                {
                    var user = await _context.user
                        .Include(u => u.role)
                        .Include(u => u.department)
                        .FirstOrDefaultAsync(u => u.user_id == signatory.user!.user_id);
                    approvers.Add(_mapper.Map<GetUserDto>(user!));
                }

                var data = new GetTicketDto()
                {
                    ticket = _mapper.Map<TicketDto>(ticket),
                    signatories = approvers
                };

                responseData.Add(data);
            }

            response.data = responseData;

            return response;
        }

        public async Task<ServiceResponse<ICollection<GetTicketDto>>> GetTodayTickets()
        {
            var response = new ServiceResponse<ICollection<GetTicketDto>>();
            var responseData = new Collection<GetTicketDto>();
            var tickets = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.user)
                .Include(u => u.user.role)
                .Include(u => u.user.department)
                .Where(t => t.date_created.Equals(DateTime.Today.Date))
                .Select(t => t)
                .ToListAsync();

            foreach (var ticket in tickets)
            {
                var approvers = new Collection<GetUserDto>();
                var signatories = await _context.approver
                    .Include(a => a.user)
                    .Include(a => a.ticket)
                    .Where(a => a.ticket!.ticket_id == ticket.ticket_id)
                    .Select(a => a)
                    .ToListAsync();
                /* var _signatories = signatories.Where(s => s.ticket!.ticket_id == ticket.ticket_id); */
                foreach (var signatory in signatories)
                {
                    var user = await _context.user
                        .Include(u => u.role)
                        .Include(u => u.department)
                        .FirstOrDefaultAsync(u => u.user_id == signatory.user!.user_id);
                    approvers.Add(_mapper.Map<GetUserDto>(user!));
                }

                var data = new GetTicketDto()
                {
                    ticket = _mapper.Map<TicketDto>(ticket),
                    signatories = approvers
                };

                responseData.Add(data);
            }

            response.data = responseData;

            return response;
        }
    }
}

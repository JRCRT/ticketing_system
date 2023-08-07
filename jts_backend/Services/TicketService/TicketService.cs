using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using jts_backend.Context;
using jts_backend.Dtos.FileDto;
using jts_backend.Dtos.SignatoryDto;
using jts_backend.Dtos.TicketDto;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;
using jts_backend.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using jts_backend.Hub;
using jts_backend.Dtos.StatusDto;

namespace jts_backend.Services.TicketService
{
    public class TicketService : ITicketService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;

        private readonly IWebHostEnvironment _env;
        private readonly IHubContext<JtsHub, IJtsHub> _hubContext;

        public TicketService(
            JtsContext context,
            IMapper mapper,
            IWebHostEnvironment env,
            IHubContext<JtsHub, IJtsHub> hubContext
        )
        {
            _context = context;
            _mapper = mapper;
            _env = env;
            _hubContext = hubContext;
        }

        public async Task<ServiceResponse<GetTicketDto>> CreateTicket(CreateTicketDto request)
        {
            var response = new ServiceResponse<GetTicketDto>();

            try
            {
                var preparedBy = await _context.user
                    .Include(u => u.role)
                    .Include(u => u.department)
                    .Include(u => u.job_title)
                    .FirstOrDefaultAsync(u => u.user_id == request.user_id);

                var priority = await _context.priority.FirstOrDefaultAsync(
                    p => p.priority_id == request.priority_id
                );

                var status = await _context.status.FirstOrDefaultAsync(
                    s => s.status_id == request.status_id
                );

                if (preparedBy == null)
                {
                    response.message = "No prepared by";
                    response.success = false;
                    return response;
                }

                if (priority == null)
                {
                    response.message = "No priority";
                    response.success = false;
                    return response;
                }

                if (status == null)
                {
                    response.message = "No status";
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
                    date_created = request.date_created.Date,
                    others = request.others
                };

                await _context.ticket.AddAsync(ticket);
                await _context.SaveChangesAsync();

                var signatories = await GetSignatories(request.signatories, ticket);
                var files = await GetFiles(request.files, ticket);

                var responseData = new GetTicketDto()
                {
                    ticket = _mapper.Map<TicketDto>(ticket),
                    signatories = signatories,
                    files = files
                };

                await _hubContext.Clients.All.GetTicket(responseData);
                await _hubContext.Clients.Client(request.connection_id).GetMyTicket(responseData);
                response.data = responseData;
                response.message = "Ticket created successfully";

                return response;
            }
            catch (Exception e)
            {
                response.message = e.Message;
                return response;
            }
        }

        public async Task<ServiceResponse<ICollection<GetTicketDto>>> GetAllTickets()
        {
            var response = new ServiceResponse<ICollection<GetTicketDto>>();
            var tickets = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.user)
                .Include(u => u.user.role)
                .Include(u => u.user.department)
                .Include(u => u.user.job_title)
                .Select(t => t)
                .ToListAsync();
            var data = await GetTicketsData(tickets);
            response.data = data;

            return response;
        }

        public async Task<ServiceResponse<ICollection<GetTicketDto>>> GetTicketByStatus(
            string status
        )
        {
            var response = new ServiceResponse<ICollection<GetTicketDto>>();
            var tickets = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.user)
                .Include(u => u.user.role)
                .Include(u => u.user.department)
                .Where(t => t.status.name.Equals(status))
                .Select(t => t)
                .ToListAsync();

            var data = await GetTicketsData(tickets);
            response.data = data;

            return response;
        }

        public async Task<ServiceResponse<ICollection<GetTicketDto>>> GetTodayTickets()
        {
            var response = new ServiceResponse<ICollection<GetTicketDto>>();
            var tickets = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.user)
                .Include(u => u.user.role)
                .Include(u => u.user.department)
                .Include(u => u.user.job_title)
                .Where(t => t.date_created.Equals(DateTime.Today.Date))
                .Select(t => t)
                .ToListAsync();
            var data = await GetTicketsData(tickets);
            response.data = data;

            return response;
        }

        public async Task<ServiceResponse<GetTicketDto>> GetTicketById(int id)
        {
            var response = new ServiceResponse<GetTicketDto>();
            var tickets = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.user)
                .Include(u => u.user.role)
                .Include(u => u.user.department)
                .Include(u => u.user.job_title)
                .FirstOrDefaultAsync(t => t.ticket_id == id);

            var data = await GetTicketData(tickets!);
            response.data = data;

            return response;
        }

        public async Task<ServiceResponse<ICollection<GetTicketDto>>> GetTicketByUser(
            TicketByUserDto request
        )
        {
            var response = new ServiceResponse<ICollection<GetTicketDto>>();
            var tickets = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.user)
                .Include(u => u.user.role)
                .Include(u => u.user.department)
                .Include(u => u.user.job_title)
                .Where(
                    t =>
                        t.user.user_id == request.user_id && t.status.status_id == request.status_id
                )
                .Select(t => t)
                .ToListAsync();

            var data = await GetTicketsData(tickets);
            response.data = data;

            return response;
        }

        public async Task<ServiceResponse<ICollection<GetTicketDto>>> GetTicketsForApproval(
            int userId,
            string status
        )
        {
            var response = new ServiceResponse<ICollection<GetTicketDto>>();
            var responseData = new Collection<GetTicketDto>();
            var tickets = await _context.approver
                .Include(t => t.status)
                .Include(a => a.ticket!.user)
                .Include(a => a.ticket!.user.department)
                .Include(a => a.ticket!.user.role)
                .Include(a => a.ticket!.user.job_title)
                .Include(a => a.user)
                .Include(u => u.user!.department)
                .Include(u => u.user!.job_title)
                .Include(u => u.user!.role)
                .Include(a => a.ticket)
                .Include(t => t.ticket!.status)
                .Include(t => t.ticket!.priority)
                .Where(a => a.user!.user_id == userId && a.status.name == status)
                .Select(a => a.ticket)
                .ToListAsync();

            var data = await GetTicketsData(tickets!);
            response.data = data;
            return response;
        }

        public async Task<ServiceResponse<GetTicketDto>> ApproveTicket(ApproveTicketDto request)
        {
            const int APPROVED_STATUS_ID = 2;
            var response = new ServiceResponse<GetTicketDto>();
            var approvedStatus = await _context.status.FirstOrDefaultAsync(
                s => s.status_id == APPROVED_STATUS_ID
            );

            var signatory = await _context.approver
                .Include(t => t.status)
                .Include(a => a.ticket!.user)
                .Include(a => a.ticket!.user.department)
                .Include(a => a.ticket!.user.role)
                .Include(a => a.ticket!.user.job_title)
                .Include(a => a.user)
                .Include(u => u.user!.department)
                .Include(u => u.user!.job_title)
                .Include(u => u.user!.role)
                .Include(a => a.ticket)
                .Include(t => t.ticket!.status)
                .Include(t => t.ticket!.priority)
                .FirstOrDefaultAsync(s => s.signatory_id == request.signatory_id);

            signatory!.status = approvedStatus!;
            signatory!.action_date = DateTime.Now;

            _context.approver.Update(signatory!);
            await _context.SaveChangesAsync();

            bool isApprovedByAll = true;
            var signatories = await _context.approver
                .Include(s => s.ticket)
                .Include(s => s.status)
                .Where(t => t.ticket!.ticket_id == signatory.ticket!.ticket_id)
                .Select(s => s)
                .ToListAsync();

            foreach (var _signatory in signatories)
            {
                if (_signatory.status.status_id != APPROVED_STATUS_ID)
                {
                    isApprovedByAll = false;
                }
            }

            if (isApprovedByAll)
            {
                var ticket = await _context.ticket.FirstOrDefaultAsync(
                    t => t.ticket_id == signatory!.ticket!.ticket_id
                );

                ticket!.status = approvedStatus!;
                ticket!.date_approved = DateTime.Now;
                _context.ticket.Update(ticket);
                await _context.SaveChangesAsync();
            }

            var ticketForApproval = await GetTicketData(signatory!.ticket!);
            await _hubContext.Clients
                .Client(request.connection_id)
                .GetTicketForApproval(ticketForApproval);

            response.data = ticketForApproval;
            response.message = "Approved Successfully";
            return response;
        }

        public async Task<ServiceResponse<GetTicketDto>> DeclineTicket(DeclineTicketDto request)
        {
            var response = new ServiceResponse<GetTicketDto>();
            const int DECLINE_STATUS_ID = 3;
            var declineStatus = await _context.status.FirstOrDefaultAsync(
                s => s.status_id == DECLINE_STATUS_ID
            );

            var signatory = await _context.approver
                .Include(t => t.status)
                .Include(a => a.ticket!.user)
                .Include(a => a.ticket!.user.department)
                .Include(a => a.ticket!.user.role)
                .Include(a => a.ticket!.user.job_title)
                .Include(a => a.user)
                .Include(u => u.user!.department)
                .Include(u => u.user!.job_title)
                .Include(u => u.user!.role)
                .Include(a => a.ticket)
                .Include(t => t.ticket!.status)
                .Include(t => t.ticket!.priority)
                .FirstOrDefaultAsync(s => s.signatory_id == request.signatory_id);

            signatory!.status = declineStatus!;
            signatory!.action_date = DateTime.Now;

            _context.approver.Update(signatory!);
            await _context.SaveChangesAsync();

            bool isApprovedByAll = true;
            var signatories = await _context.approver
                .Include(s => s.ticket)
                .Include(s => s.status)
                .Where(t => t.ticket!.ticket_id == signatory.ticket!.ticket_id)
                .Select(s => s)
                .ToListAsync();

            foreach (var _signatory in signatories)
            {
                if (_signatory.status.status_id != DECLINE_STATUS_ID)
                {
                    isApprovedByAll = false;
                }
            }

            if (isApprovedByAll)
            {
                var ticket = await _context.ticket.FirstOrDefaultAsync(
                    t => t.ticket_id == signatory!.ticket!.ticket_id
                );

                ticket!.status = declineStatus!;
                ticket!.date_approved = DateTime.Now;
                _context.ticket.Update(ticket);
                await _context.SaveChangesAsync();
            }

            var ticketForApproval = await GetTicketData(signatory!.ticket!);
            await _hubContext.Clients
                .Client(request.connection_id)
                .GetTicketForApproval(ticketForApproval);

            response.data = ticketForApproval;
            response.message = "Approved Successfully";
            return response;
        }

        private async Task<ICollection<GetSignatoryDto>> GetSignatories(
            List<CreateSignatoryDto> signatories,
            TicketModel ticket
        )
        {
            var status = await _context.status.FirstOrDefaultAsync(s => s.status_id == 1);
            var _signatories = new Collection<GetSignatoryDto>();
            foreach (var signatory in signatories)
            {
                var user = await _context.user
                    .Include(u => u.role)
                    .Include(u => u.department)
                    .Include(u => u.job_title)
                    .FirstOrDefaultAsync(u => u.user_id == signatory.user_id);

                var newSignatory = new SignatoryModel()
                {
                    ticket = ticket,
                    user = user,
                    type = signatory.type,
                    status = status!
                };

                await _context.approver.AddAsync(newSignatory);
                await _context.SaveChangesAsync();
                var signatoryData = new GetSignatoryDto()
                {
                    user = _mapper.Map<GetUserDto>(user),
                    type = signatory.type,
                };
                _signatories.Add(signatoryData);
            }

            return _signatories;
        }

        private async Task<ICollection<GetFileDto>> GetFiles(
            List<IFormFile> files,
            TicketModel ticket
        )
        {
            var _files = new Collection<GetFileDto>();
            foreach (var file in files)
            {
                var fileData = await Helper.Helper.UploadFiles(file, _env.ContentRootPath, ticket);
                await _context.file.AddAsync(fileData);
                await _context.SaveChangesAsync();
                _files.Add(_mapper.Map<GetFileDto>(fileData));
            }

            return _files;
        }

        private async Task<Collection<GetTicketDto>> GetTicketsData(List<TicketModel> tickets)
        {
            var responseData = new Collection<GetTicketDto>();
            foreach (var ticket in tickets)
            {
                var approvers = new Collection<GetSignatoryDto>();
                var signatories = await _context.approver
                    .Include(a => a.user)
                    .Include(a => a.ticket)
                    .Include(a => a.status)
                    .Where(a => a.ticket!.ticket_id == ticket.ticket_id)
                    .Select(a => a)
                    .ToListAsync();

                var files = await _context.file
                    .Where(f => f.ticket.ticket_id == ticket.ticket_id)
                    .Select(f => _mapper.Map<GetFileDto>(f))
                    .ToListAsync();

                foreach (var signatory in signatories)
                {
                    var user = await _context.user
                        .Include(u => u.role)
                        .Include(u => u.department)
                        .Include(u => u.job_title)
                        .FirstOrDefaultAsync(u => u.user_id == signatory.user!.user_id);
                    var approverData = new GetSignatoryDto()
                    {
                        signatory_id = signatory.signatory_id,
                        user = _mapper.Map<GetUserDto>(user!),
                        type = signatory.type,
                        status = _mapper.Map<GetStatusDto>(signatory.status)
                    };
                    approvers.Add(approverData);
                }

                var data = new GetTicketDto()
                {
                    ticket = _mapper.Map<TicketDto>(ticket),
                    signatories = approvers,
                    files = files
                };

                responseData.Add(data);
            }
            return responseData;
        }

        private async Task<GetTicketDto> GetTicketData(TicketModel ticket)
        {
            var approvers = new Collection<GetSignatoryDto>();
            var signatories = await _context.approver
                .Include(a => a.user)
                .Include(a => a.ticket)
                .Include(a => a.status)
                .Where(a => a.ticket!.ticket_id == ticket.ticket_id)
                .Select(a => a)
                .ToListAsync();

            var files = await _context.file
                .Where(f => f.ticket.ticket_id == ticket.ticket_id)
                .Select(f => _mapper.Map<GetFileDto>(f))
                .ToListAsync();

            foreach (var signatory in signatories)
            {
                var user = await _context.user
                    .Include(u => u.role)
                    .Include(u => u.department)
                    .Include(u => u.job_title)
                    .FirstOrDefaultAsync(u => u.user_id == signatory.user!.user_id);
                var approverData = new GetSignatoryDto()
                {
                    signatory_id = signatory.signatory_id,
                    user = _mapper.Map<GetUserDto>(user!),
                    type = signatory.type,
                    status = _mapper.Map<GetStatusDto>(signatory.status)
                };
                approvers.Add(approverData);
            }

            var data = new GetTicketDto()
            {
                ticket = _mapper.Map<TicketDto>(ticket),
                signatories = approvers,
                files = files
            };

            return data;
        }
    }
}

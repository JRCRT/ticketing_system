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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using jts_backend.Hub;
using jts_backend.Dtos.StatusDto;
using jts_backend.Enums;
using jts_backend.Configuration;
using Microsoft.Extensions.Options;

namespace jts_backend.Services.TicketService
{
    public class TicketService : ITicketService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHubContext<JtsHub, IJtsHub> _hubContext;
        private readonly AppSettings _settings;

        public TicketService(
            JtsContext context,
            IMapper mapper,
            IWebHostEnvironment env,
            IHubContext<JtsHub, IJtsHub> hubContext,
            IOptions<AppSettings> settings
        )
        {
            _context = context;
            _mapper = mapper;
            _env = env;
            _hubContext = hubContext;
            _settings = settings.Value;
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
                    .FirstOrDefaultAsync(u => u.user_id == request.created_by);

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

                var ticketData = new TicketModel()
                {
                    background = request.background,
                    content = request.content,
                    rejection_reason = null,
                    reason = request.reason,
                    subject = request.subject,
                    condition = request.condition,
                    priority = priority,
                    status = status,
                    received_by = null,
                    rejected_by = null,
                    created_by = preparedBy,
                    date_created = request.date_created.Date,
                    others = request.others
                };

                await _context.ticket.AddAsync(ticketData);
                await _context.SaveChangesAsync();

                var signatories = await GetSignatories(request.signatories, ticketData);
                var files = await GetFiles(request.files, ticketData.ticket_id, OwnerType.Ticket);
                var ticket = new TicketDto()
                {
                    background = request.background,
                    condition = request.background,
                    content = request.content,
                    action_date = request.action_date,
                    date_created = request.date_created,
                    rejection_reason = null,
                    others = request.others,
                    priority = priority,
                    status = status,
                    reason = request.reason,
                    subject = request.subject,
                    ticket_id = ticketData.ticket_id,
                    created_by = await GetUserData(preparedBy.user_id),
                    received_by = null,
                    rejected_by = null
                };

                var responseData = new GetTicketDto()
                {
                    ticket = ticket,
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

        public async Task<ServiceResponse<GetTicketsDto>> GetAllTickets()
        {
            var response = new ServiceResponse<GetTicketsDto>();
            var tickets = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.created_by)
                .Include(u => u.created_by.role)
                .Include(u => u.created_by.department)
                .Include(u => u.created_by.job_title)
                .Select(t => t)
                .ToListAsync();
            var data = await GetTicketsData(tickets, tickets.Count, null);
            response.data = data;

            return response;
        }

        public async Task<ServiceResponse<GetTicketsDto>> GetTicketByStatus(
            TicketByStatusDto request
        )
        {
            var response = new ServiceResponse<GetTicketsDto>();
            var result = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.created_by)
                .Include(u => u.created_by.role)
                .Include(u => u.created_by.department)
                .Where(t => t.status.status_id == request.status_id)
                .Select(t => t)
                .ToListAsync();
            var totalResult = await _context.ticket
                .Include(t => t.status)
                .Where(t => t.status.status_id == request.status_id)
                .Select(t => t)
                .ToListAsync();

            var totalTickets = 0;
            var tickets = new List<TicketModel>();

            if (request.items_per_page == 0)
            {
                totalTickets = totalResult.Count;
            }
            else
            {
                if (
                    request.ticket_id == 0
                    && request.date_created.Date.Equals(DateTime.Parse("1/1/1"))
                    && request.prepared_by == 0
                )
                {
                    tickets = result.Skip(request.offset).Take(request.items_per_page).ToList();
                    totalTickets = totalResult.Count;
                }
                else if (
                    request.ticket_id != 0
                    && !request.date_created.Date.Equals(DateTime.Parse("1/1/1"))
                    && request.prepared_by != 0
                )
                {
                    tickets = result
                        .Where(
                            t =>
                                t.ticket_id == request.ticket_id
                                && t.date_created.Date.Equals(request.date_created.Date)
                                && t.created_by.user_id == request.prepared_by
                        )
                        .Skip(request.offset)
                        .Take(request.items_per_page)
                        .Select(t => t)
                        .ToList();

                    totalTickets = totalResult
                        .Where(
                            t =>
                                t.ticket_id == request.ticket_id
                                && t.date_created.Date.Equals(request.date_created.Date)
                                && t.created_by.user_id == request.prepared_by
                        )
                        .Count();
                }
                else
                {
                    if (request.ticket_id != 0)
                    {
                        tickets = result
                            .Where(t => t.ticket_id == request.ticket_id)
                            .Skip(request.offset)
                            .Take(request.items_per_page)
                            .Select(t => t)
                            .ToList();
                        totalTickets = totalResult
                            .Where(t => t.ticket_id == request.ticket_id)
                            .Count();
                    }
                    else if (!request.date_created.Date.Equals(DateTime.Parse("1/1/1")))
                    {
                        tickets = result
                            .Where(t => t.date_created.Date.Equals(request.date_created.Date))
                            .Skip(request.offset)
                            .Take(request.items_per_page)
                            .Select(t => t)
                            .ToList();
                        totalTickets = totalResult
                            .Where(t => t.date_created.Date.Equals(request.date_created.Date))
                            .Count();
                    }
                    else if (request.prepared_by != 0)
                    {
                        tickets = result
                            .Where(t => t.created_by.user_id == request.prepared_by)
                            .Skip(request.offset)
                            .Take(request.items_per_page)
                            .Select(t => t)
                            .ToList();
                        totalTickets = totalResult
                            .Where(t => t.created_by.user_id == request.prepared_by)
                            .Count();
                    }
                }
            }

            var data = await GetTicketsData(tickets, totalTickets, null);
            response.data = data;

            return response;
        }

        public async Task<ServiceResponse<GetTicketsDto>> GetTodayTickets(TicketsTodayDto request)
        {
            var response = new ServiceResponse<GetTicketsDto>();

            var result = await _context.approver
                .Include(a => a.user)
                .Include(a => a.ticket)
                .Include(a => a.ticket.created_by)
                .Where(a => a.ticket.date_created.Date.Equals(DateTime.Today.Date))
                .Select(a => a)
                .ToListAsync();

            var totalResult = await _context.approver
                .Include(a => a.user)
                .Include(a => a.ticket)
                .Include(a => a.ticket.created_by)
                .Where(a => a.ticket.date_created.Date.Equals(DateTime.Today.Date))
                .Select(a => a)
                .ToListAsync();

            var tickets = new List<TicketModel>();
            var totalTickets = 0;

            switch (request.role_id)
            {
                //admin
                case 1:
                    tickets = result
                        .Select(a => a.ticket!)
                        .Distinct()
                        .Skip(request.offset)
                        .Take(request.items_per_page)
                        .ToList();
                    totalTickets = totalResult.Select(a => a.ticket).Distinct().Count();
                    break;
                //approver
                case 2:
                    tickets = result
                        .Where(a => a.user.user_id == request.user_id && a.can_approve == true)
                        .Select(a => a.ticket!)
                        .Skip(request.offset)
                        .Take(request.items_per_page)
                        .ToList();
                    totalTickets = totalResult
                        .Where(a => a.user.user_id == request.user_id)
                        .Select(a => a.ticket)
                        .Count();
                    break;
                //checker
                case 3:
                    tickets = result
                        .Where(a => a.user.user_id == request.user_id)
                        .Select(a => a.ticket!)
                        .Skip(request.offset)
                        .Take(request.items_per_page)
                        .ToList();
                    totalTickets = totalResult
                        .Where(a => a.user.user_id == request.user_id)
                        .Select(a => a.ticket)
                        .Count();
                    break;
                //user
                case 4:
                    tickets = result
                        .Where(a => a.ticket.created_by.user_id == request.user_id)
                        .Select(a => a.ticket!)
                        .Distinct()
                        .Skip(request.offset)
                        .Take(request.items_per_page)
                        .ToList();
                    totalTickets = totalResult
                        .Where(a => a.ticket.created_by.user_id == request.user_id)
                        .Select(a => a.ticket!)
                        .Distinct()
                        .Count();
                    break;
            }

            var data = await GetTicketsData(tickets, totalTickets, null);
            response.data = data;
            return response;
        }

        public async Task<ServiceResponse<GetTicketDto>> GetTicketById(int id)
        {
            var response = new ServiceResponse<GetTicketDto>();
            var tickets = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.created_by)
                .Include(t => t.received_by)
                .Include(t => t.rejected_by)
                .Include(u => u.created_by.role)
                .Include(u => u.created_by.department)
                .Include(u => u.created_by.job_title)
                .FirstOrDefaultAsync(t => t.ticket_id == id);

            var data = await GetTicketData(tickets!);
            response.data = data;

            return response;
        }

        public async Task<ServiceResponse<GetTicketsDto>> GetTicketByUser(TicketByUserDto request)
        {
            var result = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.created_by)
                .Include(u => u.created_by.role)
                .Include(u => u.created_by.department)
                .Include(u => u.created_by.job_title)
                .OrderBy(t => t.ticket_id)
                .Where(
                    t =>
                        t.created_by.user_id == request.user_id
                        && t.status.status_id == request.status_id
                )
                .Select(t => t)
                .ToListAsync();

            var totalResult = await _context.ticket
                .Where(
                    t =>
                        t.created_by.user_id == request.user_id
                        && t.status.status_id == request.status_id
                )
                .ToListAsync();

            var totalTickets = 0;
            var tickets = new List<TicketModel>();
            var response = new ServiceResponse<GetTicketsDto>();

            if (request.items_per_page == 0)
            {
                totalTickets = totalResult.Count;
            }
            else
            {
                if (
                    request.ticket_id == 0
                    && request.date_created.Date.Equals(DateTime.Parse("1/1/1"))
                )
                {
                    tickets = result.Skip(request.offset).Take(request.items_per_page).ToList();
                    totalTickets = totalResult.Count;
                }
                else if (
                    request.ticket_id != 0
                    && !request.date_created.Date.Equals(DateTime.Parse("1/1/1"))
                )
                {
                    tickets = result
                        .Where(
                            t =>
                                t.ticket_id == request.ticket_id
                                && t.date_created.Date.Equals(request.date_created.Date)
                        )
                        .Skip(request.offset)
                        .Take(request.items_per_page)
                        .Select(t => t)
                        .ToList();

                    totalTickets = totalResult
                        .Where(
                            t =>
                                t.ticket_id == request.ticket_id
                                && t.date_created.Date.Equals(request.date_created.Date)
                        )
                        .Count();
                }
                else
                {
                    if (request.ticket_id != 0)
                    {
                        tickets = result
                            .Where(t => t.ticket_id == request.ticket_id)
                            .Skip(request.offset)
                            .Take(request.items_per_page)
                            .Select(t => t)
                            .ToList();
                        totalTickets = totalResult
                            .Where(t => t.ticket_id == request.ticket_id)
                            .Count();
                    }
                    else if (!request.date_created.Date.Equals(DateTime.Parse("1/1/1")))
                    {
                        tickets = result
                            .Where(t => t.date_created.Date.Equals(request.date_created.Date))
                            .Skip(request.offset)
                            .Take(request.items_per_page)
                            .Select(t => t)
                            .ToList();
                        totalTickets = totalResult
                            .Where(t => t.date_created.Date.Equals(request.date_created.Date))
                            .Count();
                    }
                }
            }

            var data = await GetTicketsData(tickets, totalTickets, null);
            response.message = request.date_created.Date.ToString();
            response.data = data;

            return response;
        }

        public async Task<ServiceResponse<GetTicketsDto>> GetTicketsForApproval(
            TicketByUserDto request
        )
        {
            var response = new ServiceResponse<GetTicketsDto>();
            var result = await _context.approver
                .Include(t => t.status)
                .Include(a => a.ticket!.created_by)
                .Include(a => a.ticket!.created_by.department)
                .Include(a => a.ticket!.created_by.role)
                .Include(a => a.ticket!.created_by.job_title)
                .Include(a => a.user)
                .Include(u => u.user!.department)
                .Include(u => u.user!.job_title)
                .Include(u => u.user!.role)
                .Include(a => a.ticket)
                .Include(t => t.ticket.status)
                .Include(t => t.ticket.priority)
                .OrderBy(t => t.ticket.ticket_id)
                .Where(
                    a =>
                        a.user!.user_id == request.user_id
                        && a.status.status_id == request.status_id
                        && a.can_approve == true
                )
                .Select(a => a.ticket)
                .ToListAsync();

            var date = await _context.approver.FirstOrDefaultAsync(
                a => a.user.user_id == request.user_id
            );
            DateTime? actionDate = new DateTime();

            if (date?.action_date == null)
            {
                actionDate = null;
            }
            else
            {
                actionDate = date.action_date;
            }

            var totalResult = await _context.approver
                .Include(a => a.ticket)
                .Include(a => a.ticket.created_by)
                .Where(
                    a =>
                        a.user!.user_id == request.user_id
                        && a.status.status_id == request.status_id
                        && a.can_approve == true
                )
                .Select(a => a.ticket)
                .ToListAsync();

            var totalTickets = 0;
            var tickets = new List<TicketModel?>();
            if (request.items_per_page == 0)
            {
                totalTickets = totalResult.Count;
            }
            else
            {
                if (
                    request.ticket_id == 0
                    && request.date_created.Date.Equals(DateTime.Parse("1/1/1"))
                    && request.prepared_by == 0
                )
                {
                    tickets = result
                        .Skip(request.offset)
                        .Take(request.items_per_page)
                        .Select(t => t)
                        .ToList();
                    totalTickets = totalResult.Count;
                }
                else if (
                    request.ticket_id != 0
                    && !request.date_created.Date.Equals(DateTime.Parse("1/1/1"))
                    && request.prepared_by != 0
                )
                {
                    tickets = result
                        .Where(
                            t =>
                                t.ticket_id == request.ticket_id
                                && t.date_created.Date.Equals(request.date_created.Date)
                                && t.created_by.user_id == request.prepared_by
                        )
                        .Skip(request.offset)
                        .Take(request.items_per_page)
                        .Select(t => t)
                        .ToList();

                    totalTickets = totalResult
                        .Where(
                            t =>
                                t.ticket_id == request.ticket_id
                                && t.date_created.Date.Equals(request.date_created.Date)
                                && t.created_by.user_id == request.prepared_by
                        )
                        .Count();
                }
                else
                {
                    if (request.ticket_id != 0)
                    {
                        tickets = result
                            .Where(t => t.ticket_id == request.ticket_id)
                            .Skip(request.offset)
                            .Take(request.items_per_page)
                            .Select(t => t)
                            .ToList();
                        totalTickets = totalResult
                            .Where(t => t.ticket_id == request.ticket_id)
                            .Count();
                    }
                    else if (!request.date_created.Date.Equals(DateTime.Parse("1/1/1")))
                    {
                        tickets = result
                            .Where(t => t.date_created.Date.Equals(request.date_created.Date))
                            .Skip(request.offset)
                            .Take(request.items_per_page)
                            .Select(t => t)
                            .ToList();
                        totalTickets = totalResult
                            .Where(t => t.date_created.Date.Equals(request.date_created.Date))
                            .Count();
                    }
                    else if (request.prepared_by != 0)
                    {
                        tickets = result
                            .Where(t => t.created_by.user_id == request.prepared_by)
                            .Skip(request.offset)
                            .Take(request.items_per_page)
                            .Select(t => t)
                            .ToList();
                        totalTickets = totalResult
                            .Where(t => t.created_by.user_id == request.prepared_by)
                            .Count();
                    }
                }
            }

            var data = await GetTicketsData(tickets, totalTickets, actionDate);
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
                .Include(a => a.ticket!.created_by)
                .Include(a => a.ticket!.created_by.department)
                .Include(a => a.ticket!.created_by.role)
                .Include(a => a.ticket!.created_by.job_title)
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
            bool isApprovedByAllChecker = true;
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

                if (
                    _signatory.status.status_id != APPROVED_STATUS_ID
                    && _signatory.type.Equals("Checker")
                )
                {
                    isApprovedByAllChecker = false;
                }
            }

            if (isApprovedByAllChecker)
            {
                signatories.ForEach(s =>
                {
                    if (!s.type.Equals("Checker"))
                    {
                        s.can_approve = true;
                        _context.approver.Update(s);
                    }
                });
            }

            if (isApprovedByAll)
            {
                var ticket = await _context.ticket.FirstOrDefaultAsync(
                    t => t.ticket_id == signatory!.ticket!.ticket_id
                );

                ticket!.status = approvedStatus!;
                ticket!.action_date = DateTime.Now;
                _context.ticket.Update(ticket);
            }

            await _context.SaveChangesAsync();
            var ticketForApproval = await GetTicketData(signatory!.ticket!);
            await _hubContext.Clients
                .Client(request.connection_id)
                .GetTicketForApproval(ticketForApproval);

            response.data = ticketForApproval;
            response.message = "Approved Successfully";
            return response;
        }

        public async Task<ServiceResponse<GetTicketDto>> DoneTicket(DoneTicketDto request)
        {
            var response = new ServiceResponse<GetTicketDto>();
            const int DONE_STATUS_ID = 4;
            var doneStatus = await _context.status.FirstOrDefaultAsync(
                s => s.status_id == DONE_STATUS_ID
            );
            var receiver = await _context.user.FirstOrDefaultAsync(
                u => u.user_id == request.user_id
            );
            var ticket = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.created_by)
                .Include(u => u.created_by.role)
                .Include(u => u.created_by.department)
                .Include(u => u.created_by.job_title)
                .FirstOrDefaultAsync(t => t.ticket_id == request.ticket_id);

            var signatories = await _context.approver
                .Include(s => s.status)
                .Include(s => s.ticket)
                .Where(s => s.ticket.ticket_id == ticket.ticket_id)
                .Select(s => s)
                .ToListAsync();

            foreach (var signatory in signatories)
            {
                signatory.status = doneStatus;
                _context.Update(signatory);
            }

            ticket.received_by = receiver;
            ticket.status = doneStatus;
            _context.Update(ticket);
            await _context.SaveChangesAsync();
            var data = await GetTicketData(ticket!);

            await _hubContext.Clients.Client(request.connection_id).GetAllTicket(data);
            response.data = data;
            response.message = "Completed.";

            return response;
        }

        public async Task<ServiceResponse<GetTicketDto>> RejectTicket(RejectTicket request)
        {
            var response = new ServiceResponse<GetTicketDto>();
            const int REJECTED_STATUS_ID = 3;
            var rejectedStatus = await _context.status.FirstOrDefaultAsync(
                s => s.status_id == REJECTED_STATUS_ID
            );

            var signatory = await _context.approver
                .Include(t => t.status)
                .Include(a => a.ticket!.created_by)
                .Include(a => a.ticket!.created_by.department)
                .Include(a => a.ticket!.created_by.role)
                .Include(a => a.ticket!.created_by.job_title)
                .Include(a => a.user)
                .Include(u => u.user!.department)
                .Include(u => u.user!.job_title)
                .Include(u => u.user!.role)
                .Include(a => a.ticket)
                .Include(t => t.ticket!.status)
                .Include(t => t.ticket!.priority)
                .FirstOrDefaultAsync(s => s.signatory_id == request.signatory_id);

            signatory!.status = rejectedStatus!;
            signatory!.action_date = DateTime.Now;

            _context.approver.Update(signatory!);
            await _context.SaveChangesAsync();

            var signatories = await _context.approver
                .Include(s => s.ticket)
                .Include(s => s.status)
                .Where(t => t.ticket!.ticket_id == signatory.ticket!.ticket_id)
                .Select(s => s)
                .ToListAsync();

            //decline for all signatories
            foreach (var _signatory in signatories)
            {
                _signatory.status = rejectedStatus!;
                _signatory.action_date = DateTime.Now;
                _context.approver.Update(_signatory);
            }

            //change ticket status to decline
            var ticket = await _context.ticket
                .Where(t => t.ticket_id == signatory!.ticket!.ticket_id)
                .FirstOrDefaultAsync();

            var rejectedBy = await _context.user.FirstOrDefaultAsync(
                u => u.user_id == signatory!.user!.user_id
            );

            ticket!.action_date = DateTime.Now;
            ticket!.rejection_reason = request.rejection_reason;
            ticket!.rejected_by = rejectedBy;
            ticket!.status = rejectedStatus!;

            _context.ticket.Update(ticket);
            await _context.SaveChangesAsync();

            var ticketForApproval = await GetTicketData(signatory!.ticket!);
            await _hubContext.Clients
                .Client(request.connection_id)
                .GetTicketForApproval(ticketForApproval);

            response.data = ticketForApproval;
            response.message = "Rejected Successfully";
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
                    status = status!,
                    can_approve = GetCanApprove(signatory.type)
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

        private bool GetCanApprove(string type)
        {
            var canApprove = false;
            const string CHECKER = "Checker";
            if (type.Equals(CHECKER))
            {
                canApprove = true;
                return canApprove;
            }
            return canApprove;
        }

        private async Task<ICollection<GetFileDto>> GetFiles(
            List<IFormFile> files,
            int ownerId,
            OwnerType ownerType
        )
        {
            var _files = new Collection<GetFileDto>();
            foreach (var file in files)
            {
                var fileData = await Helper.Helper.UploadFiles(
                    file,
                    _settings.FilePath!,
                    ownerId,
                    ownerType
                );
                await _context.file.AddAsync(fileData);
                await _context.SaveChangesAsync();
                _files.Add(_mapper.Map<GetFileDto>(fileData));
            }

            return _files;
        }

        private async Task<GetTicketsDto> GetTicketsData(
            List<TicketModel> tickets,
            int totalItems,
            DateTime? actionDate
        )
        {
            ICollection<GetTicketDto> ticketLists = new Collection<GetTicketDto>();
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

                var ticketFiles = await _context.file
                    .Where(
                        f =>
                            f.owner_id == ticket.ticket_id
                            && f.owner_type.Equals(OwnerType.Ticket.ToString())
                    )
                    .Select(f => _mapper.Map<GetFileDto>(f))
                    .ToListAsync();

                foreach (var signatory in signatories)
                {
                    var user = await _context.user
                        .Include(u => u.role)
                        .Include(u => u.department)
                        .Include(u => u.job_title)
                        .Where(u => u.user_id == signatory.user!.user_id)
                        .Select(u => _mapper.Map<UserDto>(u))
                        .FirstOrDefaultAsync();

                    var approverData = new GetSignatoryDto()
                    {
                        signatory_id = signatory.signatory_id,
                        user = await GetUserData(user!.user_id),
                        type = signatory.type,
                        status = _mapper.Map<GetStatusDto>(signatory.status)
                    };
                    approvers.Add(approverData);
                }

                var _ticket = new TicketDto()
                {
                    ticket_id = ticket.ticket_id,
                    background = ticket.background,
                    condition = ticket.condition,
                    content = ticket.content,
                    action_date = ticket.action_date,
                    date_created = ticket.date_created,
                    rejection_reason = ticket.rejection_reason,
                    others = ticket.others,
                    priority = ticket.priority,
                    reason = ticket.reason,
                    status = ticket.status,
                    subject = ticket.subject,
                    created_by = await GetUserData(ticket.created_by.user_id),
                    rejected_by = await GetUserData(ticket.rejected_by.user_id),
                    received_by = await GetUserData(ticket.received_by.user_id)
                };

                var data = new GetTicketDto()
                {
                    ticket = _mapper.Map<TicketDto>(_ticket),
                    signatories = approvers,
                    files = ticketFiles,
                    action_date = actionDate
                };

                ticketLists.Add(data);
            }

            var responseData = new GetTicketsDto()
            {
                tickets = ticketLists,
                total_items = totalItems,
            };

            return responseData;
        }

        private async Task<GetUserDto> GetUserData(int userId)
        {
            var userData = new GetUserDto();
            var user = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
                .Include(u => u.role)
                .FirstOrDefaultAsync(u => u.user_id == userId);
            if (user != null)
            {
                var file = await _context.file.FirstOrDefaultAsync(
                    f => f.owner_type.Equals(OwnerType.User.ToString()) && f.owner_id == userId
                );

                userData.user = _mapper.Map<UserDto>(user);
                userData.file = _mapper.Map<GetFileDto>(file);
            }

            return userData;
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
                .Where(
                    f =>
                        f.owner_id == ticket.ticket_id
                        && f.owner_type.Equals(OwnerType.Ticket.ToString())
                )
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
                    user = await GetUserData(user!.user_id),
                    type = signatory.type,
                    status = _mapper.Map<GetStatusDto>(signatory.status),
                    action_date = signatory.action_date
                };
                approvers.Add(approverData);
            }

            var _ticket = new TicketDto()
            {
                ticket_id = ticket.ticket_id,
                background = ticket.background,
                condition = ticket.condition,
                content = ticket.content,
                action_date = ticket.action_date,
                date_created = ticket.date_created,
                rejection_reason = ticket.rejection_reason,
                others = ticket.others,
                priority = ticket.priority,
                reason = ticket.reason,
                status = ticket.status,
                subject = ticket.subject,
                created_by = await GetUserData(ticket.created_by.user_id),
                rejected_by = await GetUserData(ticket.rejected_by.user_id),
                received_by = await GetUserData(ticket.received_by.user_id)
            };

            var data = new GetTicketDto()
            {
                ticket = _mapper.Map<TicketDto>(_ticket),
                signatories = approvers,
                files = files
            };

            return data;
        }
    }
}

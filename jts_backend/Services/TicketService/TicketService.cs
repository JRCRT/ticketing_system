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

namespace jts_backend.Services.TicketService
{
    public class TicketService : ITicketService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;

        private readonly IWebHostEnvironment _env;

        public TicketService(JtsContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
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
                /*  var serializeOptions = new JsonSerializerOptions()
                 {
                     Converters = { new StringConverter() }
                 }; */

                /* var signatories = request.signatories;
                var _signatories = new Collection<GetSignatoryDto>();

                foreach (var signatory in signatories)
                {
                    var user = await _context.user
                        .Include(u => u.role)
                        .Include(u => u.department)
                        .Include(u => u.job_title)
                        .FirstOrDefaultAsync(u => u.user_id == signatory.user_id);
                    var approver = new SignatoryModel()
                    {
                        ticket = ticket,
                        user = user,
                        type = signatory.type
                    };

                    _context.approver.Add(approver);
                    await _context.SaveChangesAsync();

                    if (user == null)
                    {
                        response.success = false;
                        response.message = "Something went wrong.";
                        return response;
                    }
                    var signatoryData = new GetSignatoryDto()
                    {
                        user = _mapper.Map<GetUserDto>(user),
                        type = signatory.type
                    };
                    _signatories.Add(signatoryData);
                } */
                /*  var json = JsonSerializer.Deserialize<List<CreateSignatoryDto>>(
                     request.signatories,
                     serializeOptions
                 ); */

                var signatories = await GetSignatories(request.signatories, ticket);
                var files = await GetFiles(request.files, ticket);
                /* var files = request.files;
                var _files = new Collection<GetFileDto>();

                foreach (var file in files)
                {
                    var fileData = await Helper.Helper.UploadFiles(
                        file,
                        _env.ContentRootPath,
                        ticket
                    );
                    _context.file.Add(fileData);
                    await _context.SaveChangesAsync();
                    _files.Add(_mapper.Map<GetFileDto>(fileData));
                } */

                var responseData = new GetTicketDto()
                {
                    ticket = _mapper.Map<TicketDto>(ticket),
                    signatories = signatories,
                    files = files
                };

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

        private async Task<ICollection<GetSignatoryDto>> GetSignatories(
            List<CreateSignatoryDto> signatories,
            TicketModel ticket
        )
        {
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
                    type = signatory.type
                };

                await _context.approver.AddAsync(newSignatory);
                await _context.SaveChangesAsync();
                var signatoryData = new GetSignatoryDto()
                {
                    user = _mapper.Map<GetUserDto>(user),
                    type = signatory.type
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
                .Include(u => u.user.job_title)
                .Select(t => t)
                .ToListAsync();

            foreach (var ticket in tickets)
            {
                var approvers = new Collection<GetSignatoryDto>();
                var signatories = await _context.approver
                    .Include(a => a.user)
                    .Include(a => a.ticket)
                    .Where(a => a.ticket!.ticket_id == ticket.ticket_id)
                    .Select(a => a)
                    .ToListAsync();

                var files = await _context.file
                    .Include(f => f.ticket)
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
                        user = _mapper.Map<GetUserDto>(user!),
                        type = signatory.type
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
                var approvers = new Collection<GetSignatoryDto>();
                var signatories = await _context.approver
                    .Include(a => a.user)
                    .Include(a => a.ticket)
                    .Where(a => a.ticket!.ticket_id == ticket.ticket_id)
                    .Select(a => a)
                    .ToListAsync();

                var files = await _context.file
                    .Include(f => f.ticket)
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
                        user = _mapper.Map<GetUserDto>(user!),
                        type = signatory.type
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
                .Include(u => u.user.job_title)
                .Where(t => t.date_created.Equals(DateTime.Today.Date))
                .Select(t => t)
                .ToListAsync();

            foreach (var ticket in tickets)
            {
                var approvers = new Collection<GetSignatoryDto>();
                var signatories = await _context.approver
                    .Include(a => a.user)
                    .Include(a => a.ticket)
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
                        user = _mapper.Map<GetUserDto>(user!),
                        type = signatory.type
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

            response.data = responseData;

            return response;
        }

        public async Task<ServiceResponse<GetTicketDto>> GetTicketById(int id)
        {
            var response = new ServiceResponse<GetTicketDto>();
            var ticket = await _context.ticket
                .Include(t => t.priority)
                .Include(t => t.status)
                .Include(t => t.user)
                .Include(u => u.user.role)
                .Include(u => u.user.department)
                .Include(u => u.user.job_title)
                .FirstOrDefaultAsync(t => t.ticket_id.Equals(id));

            var approvers = new Collection<GetSignatoryDto>();
            var signatories = await _context.approver
                .Include(a => a.user)
                .Include(a => a.ticket)
                .Where(a => a.ticket!.ticket_id == ticket!.ticket_id)
                .Select(a => a)
                .ToListAsync();

            var files = await _context.file
                .Where(f => f.ticket.ticket_id == ticket!.ticket_id)
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
                    user = _mapper.Map<GetUserDto>(user!),
                    type = signatory.type
                };
                approvers.Add(approverData);
            }

            var data = new GetTicketDto()
            {
                ticket = _mapper.Map<TicketDto>(ticket),
                signatories = approvers,
                files = files
            };

            response.data = data;

            return response;
        }
    }
}

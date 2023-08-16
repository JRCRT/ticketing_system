using System.Collections.ObjectModel;
using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.FileDto;
using jts_backend.Dtos.TicketDto;
using jts_backend.Dtos.UserDto;
using jts_backend.Enums;
using jts_backend.Hub;
using jts_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace jts_backend.Services.UserService
{
    public class UserSevice : IUserService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<JtsHub, IJtsHub> _hubContext;
        private readonly IWebHostEnvironment _env;

        public UserSevice(
            JtsContext context,
            IMapper mapper,
            IHubContext<JtsHub, IJtsHub> hubContext,
            IWebHostEnvironment env
        )
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
            _env = env;
        }

        public async Task<ServiceResponse<ICollection<GetUserDto>>> GetAllUser()
        {
            var response = new ServiceResponse<ICollection<GetUserDto>>();
            var data = new Collection<GetUserDto>();
            ICollection<UserDto> users = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
                .Include(u => u.job_title)
                .Select(u => _mapper.Map<UserDto>(u))
                .ToListAsync();

            foreach (var user in users)
            {
                var file = await _context.file.FirstOrDefaultAsync(
                    f =>
                        f.owner_id == user.user_id && f.owner_type.Equals(OwnerType.User.ToString())
                );
                var userData = new GetUserDto()
                {
                    user = user,
                    file = _mapper.Map<GetFileDto>(file)
                };

                data.Add(userData);
            }

            response.data = data;
            return response;
        }

        public async Task<ServiceResponse<TestDto>> GetUserById(int user_id)
        {
            var response = new ServiceResponse<TestDto>();
            var user = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
                .Include(u => u.job_title)
                .Where(u => u.user_id == user_id)
                .Select(u => _mapper.Map<UserDto>(u))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                response.message = "User not found.";
                response.success = false;
                return response;
            }

            var file = await _context.file.FirstOrDefaultAsync(
                f => f.owner_id == user.user_id && f.owner_type.Equals(OwnerType.User.ToString())
            );

            var path = Path.Combine(_env.ContentRootPath, "Uploads", file.stored_file_name);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var userData = new GetUserDto()
            {
                user = user,
                file = File(memory, file.content_type, Path.GetFileName(path))
            };

            response.data = userData;
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> CreateUser(CreateUserDto request)
        {
            var response = new ServiceResponse<GetUserDto>();
            var file = new GetFileDto();
            Helper.Helper.CreatePasswordHash(
                request.password,
                out byte[] passwordHash,
                out byte[] passwordSalt
            );

            var user = await _context.user.FirstOrDefaultAsync(
                u => u.username.ToLower().Equals(request.username.ToLower())
            );

            var department = await _context.department
                .Where(d => d.department_id == request.department_id)
                .FirstOrDefaultAsync();
            var role = await _context.role
                .Where(r => r.role_id == request.role_id)
                .FirstOrDefaultAsync();

            var jobTitle = await _context.jobTitle
                .Where(j => j.job_title_id == request.job_title_id)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                response.message = "Username already used.";
                response.success = false;
                return response;
            }

            if (department == null)
            {
                response.message = "Department not found.";
                response.success = false;
                return response;
            }

            if (role == null)
            {
                response.message = "Role not found.";
                response.success = false;
                return response;
            }

            if (jobTitle == null)
            {
                response.message = "Job Title not found.";
                response.success = false;
                return response;
            }

            var newUser = new UserModel()
            {
                first_name = request.first_name,
                middle_name = request.middle_name,
                last_name = request.last_name,
                username = request.username,
                email = request.email,
                password_hash = passwordHash,
                password_salt = passwordSalt,
                department = department,
                ext_name = $"{request.first_name} {request.middle_name} {request.last_name}",
                role = role,
                job_title = jobTitle
            };

            _context.user.Add(newUser);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(request?.file?.FileName))
            {
                file = await GetFile(request.file, newUser.user_id, OwnerType.User);
            }

            var data = new GetUserDto() { user = _mapper.Map<UserDto>(newUser), file = file };

            response.data = data;
            await _hubContext.Clients.All.GetUser(data);

            response.message = "User added successfully.";
            return response;
        }

        public async Task<ServiceResponse<string>> UpdateUser(UpdateUserDto request)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
                .Include(u => u.job_title)
                .FirstOrDefaultAsync(c => c.user_id == request.user_id);
            var role = await _context.role.FirstOrDefaultAsync(r => r.role_id == request.role_id);
            var department = await _context.department.FirstOrDefaultAsync(
                d => d.department_id == request.department_id
            );
            var jobTitle = await _context.jobTitle.FirstOrDefaultAsync(
                j => j.job_title_id == request.job_title_id
            );
            if (user == null)
            {
                response.message = "User not found.";
                response.success = false;
                return response;
            }
            user.username = request.username;
            user.first_name = request.first_name;
            user.middle_name = request.middle_name;
            user.last_name = request.last_name;
            user.email = request.email;
            user.ext_name = $"{request.first_name} {request.middle_name} {request.last_name}";

            if (!request.password.Equals(Convert.ToBase64String(user.password_hash)))
            {
                Helper.Helper.CreatePasswordHash(
                    request.password,
                    out byte[] passwordHash,
                    out byte[] passwordSalt
                );

                user.password_hash = passwordHash;
                user.password_salt = passwordSalt;
            }
            user.role = role!;
            user.department = department!;
            user.job_title = jobTitle!;

            _context.user.Update(user);
            await _context.SaveChangesAsync();
            response.data =
                $"RPassword: {request.password} DPassword: {Convert.ToBase64String(user.password_hash)}";
            response.message = "Successfully updated";
            return response;
        }

        public async Task<ServiceResponse<ICollection<GetUserDto>>> GetUsersByRole(string role)
        {
            var response = new ServiceResponse<ICollection<GetUserDto>>();
            ICollection<GetUserDto> users = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
                .Where(u => u.role.name.Equals(role))
                .Select(u => _mapper.Map<GetUserDto>(u))
                .ToListAsync();
            response.data = users;
            return response;
        }

        private async Task<GetFileDto> GetFile(IFormFile file, int ownerId, OwnerType ownerType)
        {
            var fileData = await Helper.Helper.UploadFiles(
                file,
                _env.ContentRootPath,
                ownerId,
                ownerType
            );
            await _context.file.AddAsync(fileData);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetFileDto>(fileData);
        }
    }
}

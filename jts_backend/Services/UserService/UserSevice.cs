using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.UserDto;
using jts_backend.Hub;
using jts_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services.UserService
{
    public class UserSevice : IUserService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<JtsHub, IJtsHub> _hubContext;
        public UserSevice(
            JtsContext context,
            IMapper mapper,
            IHubContext<JtsHub, IJtsHub> hubContext
        )
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
        }
        public async Task<ServiceResponse<ICollection<GetUserDto>>> GetAllUser()
        {
            var response = new ServiceResponse<ICollection<GetUserDto>>();
            ICollection<GetUserDto> users = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
                .Include(u => u.job_title)
                .Select(u => _mapper.Map<GetUserDto>(u))
                .ToListAsync();
            response.data = users;
            return response;
        }
        public async Task<ServiceResponse<GetUserDto>> GetUserById(int user_id)
        {
            var response = new ServiceResponse<GetUserDto>();
            var user = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
                .Include(u => u.job_title)
                .Where(u => u.user_id == user_id)
                .Select(u => _mapper.Map<GetUserDto>(u))
                .FirstOrDefaultAsync();
            if (user == null)
            {
                response.message = "User not found.";
                response.success = false;
                return response;
            }
            response.data = user;
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> CreateUser(CreateUserDto request)
        {
            var response = new ServiceResponse<GetUserDto>();
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
            var data = _mapper.Map<GetUserDto>(newUser);
            response.data = data;
            await _hubContext.Clients.All.GetUser(data);
            response.message = "User added successfully.";
            return response;
        }

        public async Task<ServiceResponse<string>> UpdateUser(UpdateUserDto updateUser)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
                .Include(u => u.job_title)
                .FirstOrDefaultAsync(c => c.user_id == updateUser.user_id);

            if (user == null)
            {
                response.message = "User not found.";
                response.success = false;
                return response;
            }

            user.first_name = updateUser.first_name;
            user.middle_name = updateUser.middle_name;
            user.last_name = updateUser.last_name;
            user.email = updateUser.email;
            user.ext_name =
                $"{updateUser.first_name} {updateUser.middle_name} {updateUser.last_name}";

            Helper.Helper.CreatePasswordHash(
                updateUser.password,
                out byte[] passwordHash,
                out byte[] passwordSalt
            );
            user.password_hash = passwordHash;
            user.password_salt = passwordSalt;
            user.role.role_id = updateUser.role_id;
            user.department.department_id = updateUser.department_id;

            _context.user.Update(user);
            await _context.SaveChangesAsync();
            response.data = "Successfully updated";
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
    }
}

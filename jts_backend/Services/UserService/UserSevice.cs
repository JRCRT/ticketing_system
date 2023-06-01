using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.UserDto;
using jts_backend.Hub.User;
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

        private readonly IHubContext<UserHub, IUserHub> _hubContext;

        public UserSevice(
            JtsContext context,
            IMapper mapper,
            IHubContext<UserHub, IUserHub> hubContext
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
                .Select(u => _mapper.Map<GetUserDto>(u))
                .ToListAsync();
            response.data = users;
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUser(int user_id)
        {
            var response = new ServiceResponse<GetUserDto>();
            var user = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
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

        public async Task<ServiceResponse<GetUserDto>> CreateUser(CreateUserDto newUser)
        {
            var response = new ServiceResponse<GetUserDto>();
            Helper.Helper.CreatePasswordHash(
                newUser.password,
                out byte[] passwordHash,
                out byte[] passwordSalt
            );

            var department = await _context.department
                .Where(d => d.department_id == newUser.department_id)
                .FirstOrDefaultAsync();
            var role = await _context.role
                .Where(r => r.role_id == newUser.role_id)
                .FirstOrDefaultAsync();

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

            var user = new UserModel()
            {
                first_name = newUser.first_name,
                middle_name = newUser.middle_name,
                last_name = newUser.last_name,
                username = newUser.username,
                email = newUser.email,
                password_hash = passwordHash,
                password_salt = passwordSalt,
                department = department,
                ext_name = $"{newUser.first_name} {newUser.middle_name} {newUser.last_name}",
                role = role
            };

            _context.user.Add(user);
            await _context.SaveChangesAsync();
            response.data = _mapper.Map<GetUserDto>(user);
            await _hubContext.Clients.All.GetUser(response.data);
            response.message = "User added successfully.";
            return response;
        }

        public async Task<ServiceResponse<string>> UpdateUser(UpdateUserDto updateUser)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.user
                .Include(u => u.role)
                .Include(u => u.department)
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

            return response;
        }
    }
}

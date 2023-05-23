using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services.UserService
{
    public class UserSevice : IUserService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;

        public UserSevice(JtsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ICollection<GetUserDto>>> GetAllUser()
        {
            ServiceResponse<ICollection<GetUserDto>> response =
                new ServiceResponse<ICollection<GetUserDto>>();
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
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
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

        public async Task<ServiceResponse<string>> CreateUser(CreateUserDto newUser)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            Helper.Helper.CreatePasswordHash(
                newUser.password,
                out byte[] passwordHash,
                out byte[] passwordSalt
            );

            DepartmentModel? department = await _context.department
                .Where(d => d.department_id == newUser.department_id)
                .FirstOrDefaultAsync();
            RoleModel? role = await _context.role
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

            UserModel user = new UserModel();
            user.first_name = newUser.first_name;
            user.middle_name = newUser.middle_name;
            user.last_name = newUser.last_name;
            user.username = newUser.username;
            user.email = newUser.email;
            user.password_hash = passwordHash;
            user.password_salt = passwordSalt;
            user.department = department;
            user.ex_name = $"{newUser.first_name} {newUser.middle_name} {newUser.last_name}";
            user.role = role;
            _context.user.Add(user);
            await _context.SaveChangesAsync();
            response.data = "User created successfully.";
            return response;
        }
    }
}

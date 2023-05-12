using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;

namespace jts_backend.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<ICollection<GetUserDto>>> GetAllUser();
        Task<ServiceResponse<UserModel>> GetUser(int user_id);
        Task<ServiceResponse<UserModel>> AddUser(CreateUserDto user);
    }
}
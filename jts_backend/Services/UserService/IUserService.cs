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
        Task<ServiceResponse<GetUserDto>> GetUserById(int user_id);
        Task<ServiceResponse<ICollection<GetUserDto>>> GetUsersByRole(string role);
        Task<ServiceResponse<GetUserDto>> CreateUser(CreateUserDto request);
        Task<ServiceResponse<string>> UpdateUser(UpdateUserDto user);
        Task<ServiceResponse<ICollection<GetUserDto>>> GetCheckers(GetCheckerDto request);
        Task<ServiceResponse<ICollection<GetUserDto>>> GetRelatedParties(int currentUserId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;

namespace jts_backend.Services
{
    public interface IUserService
    {
        Task<ICollection<GetUserDto>> GetAllUser();
        Task<UserModel> GetUser(int user_id);
        Task<UserModel> AddUser(UserModel user);
    }
}
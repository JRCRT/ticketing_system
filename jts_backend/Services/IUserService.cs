using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Models;

namespace jts_backend.Services
{
    public interface IUserService
    {
        Task<ICollection<UserModel>> GetAllUser();
        Task<UserModel> GetUser(int user_id);
        void AddUser(UserModel user);
    }
}
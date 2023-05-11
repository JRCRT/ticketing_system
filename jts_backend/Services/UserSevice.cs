using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using jts_backend.Context;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services
{
    public class UserSevice : IUserService
    {

        private readonly JtsContext _context;
        public UserSevice(JtsContext context)
        {
            _context = context;
        }
        public async Task<ICollection<UserModel>> GetAllUser()
        {
            ICollection<UserModel> users = await _context.user.Select(user => user).ToListAsync();
            return users;
        }

        public Task<UserModel> GetUser(int user_id)
        {
             throw new NotImplementedException();
        }

        public void AddUser(UserModel user){
            _context.user.Add(user);
        }

    
    }
}
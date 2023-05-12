using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;

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
        public async Task<ICollection<GetUserDto>> GetAllUser()
        {
            
            ICollection<GetUserDto> users = await _context.user.Select(user => _mapper.Map<GetUserDto>(user)).ToListAsync();
            return users;
        }

        public Task<UserModel> GetUser(int user_id)
        {
             throw new NotImplementedException();
        }

        public async Task<UserModel> AddUser(UserModel newUser){
            Helper.Helper.CreatePasswordHash(newUser.password, out byte[]passwordHash, out byte[] passwordSalt);
          
            _context.user.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }
    }
}
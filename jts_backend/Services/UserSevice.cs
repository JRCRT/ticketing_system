using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos;
using jts_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services
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
        public async Task<ICollection<UserDto>> GetAllUser()
        {
            
            ICollection<UserDto> users = await _context.user.Select(user => _mapper.Map<UserDto>(user)).ToListAsync();
            return users;
        }

        public Task<UserModel> GetUser(int user_id)
        {
             throw new NotImplementedException();
        }

        public async Task<UserModel> AddUser(UserModel newUser){
        
            _context.user.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }
    }
}
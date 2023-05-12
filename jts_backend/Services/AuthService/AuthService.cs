using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jts_backend.Context;
using jts_backend.Dtos.AuthDto;
using jts_backend.Dtos.UserDto;
using jts_backend.Helper;
using jts_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jts_backend.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly JtsContext _context;
        private readonly IMapper _mapper;
        public AuthService(JtsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetUserDto> Login(LoginDto request)
        {
            UserModel? user = await _context.user.Where(u => u.username.ToLower() == request.username.ToLower()).FirstOrDefaultAsync();
            if(user is null){
                
            }
            GetUserDto getUserDto = _mapper.Map<GetUserDto>(user);
            return getUserDto;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
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
        public async Task<ServiceResponse<GetUserDto>> Login(LoginDto request)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            UserModel? user = await _context.user.Where(u => u.username.ToLower() == request.username.ToLower()).FirstOrDefaultAsync();
            if(user is null){
                response.message = "Incorrect username/password.";
                response.success = false;
            }
            else if(!Helper.Helper.VerifyPasswordHash(request.password, user.password_hash, user.password_salt)){
                response.message = "Incorrect password/password.";
                response.success = false;        
            }else{
                GetUserDto userDto = _mapper.Map<GetUserDto>(user);
                response.data = userDto;
            }
            
            return response;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.AuthDto;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;

namespace jts_backend.Services.AuthService
{
    public interface IAuthService
    {
        Task<GetUserDto> Login(LoginDto request);
    }
}
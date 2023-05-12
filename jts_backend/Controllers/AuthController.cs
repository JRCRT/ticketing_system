using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.AuthDto;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;
using jts_backend.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Login(LoginDto request){
            ServiceResponse<GetUserDto> response =  await _authService.Login(request);
            if(!response.success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
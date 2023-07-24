using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.AuthDto;
using jts_backend.Dtos.UserDto;
using jts_backend.Models;
using jts_backend.Services.AuthService;
using jts_backend.Services.EmailService;
using Microsoft.AspNetCore.Mvc;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AuthUserDto>>> Login(LoginDto request)
        {
            var response = await _authService.Login(request);
            _emailService.Send("rito.jerico@jaccs.com.ph", "rinon.ryan@jaccs.com.ph", "Test", "Test");
            if (!response.success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

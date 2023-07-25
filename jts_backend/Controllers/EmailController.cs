using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.EmailDto;
using jts_backend.Models;
using jts_backend.Services.EmailService;
using Microsoft.AspNetCore.Mvc;

namespace jts_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
    private readonly IEmailService _emailService;
    public EmailController(IEmailService emailService)
    {
    _emailService = emailService;
    }
    
    [HttpPost("SendEmail")]
    public async Task<ActionResult<ServiceResponse<string>>> SendEmail(CreateEmailDto request){
        var response = await _emailService.Send(request);
        return Ok(response);
    }
    }
}
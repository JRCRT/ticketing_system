using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.EmailDto;
using jts_backend.Models;

namespace jts_backend.Services.EmailService
{
    public interface IEmailService
    {
        public Task<ServiceResponse<string>> Send(CreateEmailDto request);
    }
}
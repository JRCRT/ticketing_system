using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jts_backend.Services.EmailService
{
    public interface IEmailService
    {
        public void Send(string from, string to, string subject, string body);
    }
}
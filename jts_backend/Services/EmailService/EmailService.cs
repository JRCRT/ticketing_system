using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Configuration;
using jts_backend.Dtos.EmailDto;
using jts_backend.Models;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace jts_backend.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _settings;
        public EmailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task<ServiceResponse<string>> Send(CreateEmailDto request)
        {
            var response = new ServiceResponse<string>();
            
            try{
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(request.from));
            email.To.Add(MailboxAddress.Parse(request.to));
            email.Subject = request.subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.body };

            using var smtp = new SmtpClient(new ProtocolLogger (Console.OpenStandardOutput ()));
            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            
            response.data = request.body;
            response.message = "Email successfully sent";

            }catch(Exception e){
                response.message = e.Message;
                response.success = false;
            }
            return response;
        }
    }
}
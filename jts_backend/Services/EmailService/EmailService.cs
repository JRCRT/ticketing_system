using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jts_backend.Dtos.EmailDto;
using jts_backend.Models;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace jts_backend.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
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

            // send email
            using var smtp = new SmtpClient(new ProtocolLogger (Console.OpenStandardOutput ()));
            await smtp.ConnectAsync(_configuration.GetSection("AppSettings:SmtpHost").Value, int.Parse(_configuration.GetSection("AppSettings:SmtpPort").Value!), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration.GetSection("AppSettings:SmtpUser").Value, _configuration.GetSection("AppSettings:SmtpPassword").Value);
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
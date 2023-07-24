using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void Send(string from, string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            // send email
            using var smtp = new SmtpClient(new ProtocolLogger (Console.OpenStandardOutput ()));
            smtp.Connect(_configuration.GetSection("AppSettings:SmtpHost").Value, int.Parse(_configuration.GetSection("AppSettings:SmtpPort").Value!), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("AppSettings:SmtpUser").Value, _configuration.GetSection("AppSettings:SmtpPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
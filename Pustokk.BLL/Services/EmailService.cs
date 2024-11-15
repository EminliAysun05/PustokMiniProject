using Microsoft.Extensions.Configuration;
using Pustokk.BLL.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string toEmail, string subject, string emailBody)
        {
            // Set up SMTP client
            SmtpClient client = new SmtpClient(_configuration["EmailSettings:Smtp"], int.Parse(_configuration["EmailSettings:Port"]));
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["EmailSettings:Host"], _configuration["EmailSettings:Password"]);

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration["EmailSettings:Host"]);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;


            mailMessage.Body = emailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }
    }


}

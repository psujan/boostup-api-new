using Boostup.API.Services.Interfaces;
using Smtp2Go.Api;
using Smtp2Go.Api.Models.Emails;

namespace Boostup.API.Services
{
    public class EmailService:IEmailService
    {
        private readonly Smtp2GoApiService emailService;
        public EmailService(IConfiguration configuration)
        {
            this.emailService = new Smtp2GoApiService(configuration["Smtp2go:ApiKey"]); 
        }

        public async Task SendEmailAsync(string senderEmail , string receiverEmail, string subject, string body)
        {
            var email = new EmailMessage(senderEmail, receiverEmail);
            email.BodyHtml = body;
            email.Subject = subject;
            await emailService.SendEmail(email);
        }
    }
}

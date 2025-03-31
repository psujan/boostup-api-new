namespace Boostup.API.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string senderEmail, string receiverEmail, string subject, string body);
    }
}

namespace Pustokk.BLL.Services.Contracts;

public interface IEmailService
{
    void SendEmail(string toEmail, string subject, string emailBody);
}

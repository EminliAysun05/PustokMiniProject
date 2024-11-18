using Pustokk.BLL.ViewModels;

namespace Pustokk.BLL.Services.Contracts;

public interface IEmailService
{
    Task SendEmailAsync(EmailSendViewModel dto);
}

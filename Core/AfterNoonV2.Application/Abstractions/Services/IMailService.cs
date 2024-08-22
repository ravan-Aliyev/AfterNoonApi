namespace AfterNoonV2.Application.Abstractions.Services;

public interface IMailService
{
    Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = false);

    Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
}

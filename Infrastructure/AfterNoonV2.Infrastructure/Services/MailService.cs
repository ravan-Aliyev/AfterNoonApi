using System.Net;
using System.Net.Mail;
using System.Text;
using AfterNoonV2.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;

namespace AfterNoonV2.Infrastructure.Services;

public class MailService : IMailService
{
    IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = false)
    {
        MailMessage mail = new();
        mail.To.Add(to);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = isBodyHtml;
        mail.From = new("eliyevrevan18@gmail.com", "AfterNoonV2", Encoding.UTF8);

        SmtpClient client = new()
        {
            Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:password"]),
            Port = 587,
            EnableSsl = true,
            Host = "smtp.gmail.com"
        };

        await client.SendMailAsync(mail);
    }

    public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
    {
        StringBuilder mail = new();
        mail.AppendLine($"Hello<br>If you wnat reset your password click <a target={"_blank"} href='http://localhost:3000/reset-password/{userId}/{resetToken}'>here</a>");

        await SendMailAsync(to, "Reset Password", mail.ToString(), true);
    }
}

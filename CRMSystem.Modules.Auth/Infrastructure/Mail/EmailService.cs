using DotNetEnv;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace CRMSystem.Modules.Auth.Infrastructure.Mail
{
    public interface IEmailService
    {
        public Task SendingAMessage(string mail, int verificationСode);
    }
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendingAMessage(string mail, int verificationСode)
        {
            var mailName = _settings.Email;
            var mailPassword = _settings.Password;

            // От куда
            MailAddress _from = new MailAddress(mailName, "Робот");

            // Куда
            MailAddress to = new MailAddress(mail);

            MailMessage message = new MailMessage(_from, to);

            message.Subject = "Проверка почты пользователя";
            message.Body = $"Пароль для проверки: {verificationСode}";
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(mailName, mailPassword);
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(message);
            Console.WriteLine("Письмо отправлено!");
        }
    }
}

using Domain.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using MailKit.Net.Smtp;
using MVC.Chat.Configurations;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptionsSnapshot<EmailConfigurations> _emailSettings;

        public EmailService(IOptionsSnapshot<EmailConfigurations> emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync(MimeMessage mailMessage, CancellationToken cancellationToken)
        {
            string host = _emailSettings.Value.EmailHost;
            int port = _emailSettings.Value.EmailPort;
            string userName = _emailSettings.Value.EmailUsername;
            string emailPassword = _emailSettings.Value.EmailPassword;
            var socketOptions = port switch
            {
                465 => SecureSocketOptions.SslOnConnect, // Implicit SSL
                587 => SecureSocketOptions.StartTls,      // Explicit TLS (STARTTLS)
                25 => SecureSocketOptions.StartTlsWhenAvailable,
                _ => SecureSocketOptions.Auto
            };
            SmtpClient smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(host, port, socketOptions, cancellationToken);
            await smtpClient.AuthenticateAsync(userName, emailPassword, cancellationToken);
            await smtpClient.SendAsync(mailMessage, cancellationToken);
            await smtpClient.DisconnectAsync(true, cancellationToken);
        }
        private MimeMessage CreateMimeMessage(string to, string subject, string body)
        {
            string userName = _emailSettings.Value.EmailUsername;
            BodyBuilder bodyBuilder = new BodyBuilder() { HtmlBody = body };
            MimeMessage mimeMessage = new MimeMessage()
            {
                Body = bodyBuilder.ToMessageBody(),
                Subject = subject,
            };
            mimeMessage.To.Add(MailboxAddress.Parse(to));
            mimeMessage.From.Add(new MailboxAddress("Freelancer Platform", userName));

            return mimeMessage;
        }


        public async Task SendPasswordResetEmailAsync(string toEmail, string resetToken, CancellationToken cancellationToken = default)
        {
            var encodedToken = WebUtility.UrlEncode(resetToken);

            var resetLink = $"https://localhost:7200/Auth/ResetPassword?email={WebUtility.UrlEncode(toEmail)}&token={encodedToken}";

            string body = $@"
                            <h2>Reset Your Password</h2>
                            <p>We received a request to reset your password for your Freelancer Platform account.</p>
                            <p>Please click the link below to set a new password:</p>
                            <p><a href='{resetLink}'>Click here to reset your password</a></p>
                            <p>If you did not request a password reset, please ignore this email.</p>";

            string subject = "Reset Your Password - Freelancer Platform";

            MimeMessage mailMessage = CreateMimeMessage(toEmail, subject, body);
            await SendEmailAsync(mailMessage, cancellationToken);
        }
        public async Task SendEmailConfirmationAsync(string toEmail, string confirmationLink, CancellationToken cancellationToken = default)
        {
            string body = $@"
                <h2>Welcome to Freelancer Platform!</h2>
                <p>Please confirm your account by clicking the link below:</p>
                <p><a href='{confirmationLink}'>Click here to confirm your email</a></p>";
            string subject = "Confirm your email address";
            MimeMessage mailMessage = CreateMimeMessage(toEmail,subject,body);
            await SendEmailAsync(mailMessage, cancellationToken);
        }
    }
}

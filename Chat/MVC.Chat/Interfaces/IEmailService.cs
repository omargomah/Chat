using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string toEmail, string confirmationLink, CancellationToken cancellationToken = default);
        Task SendPasswordResetEmailAsync(string toEmail, string resetToken, CancellationToken cancellationToken = default);

    }
}

using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace webapphotel.Services
{
    public class EmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }

    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendVerificationEmailAsync(string email, string token);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmailService(IOptions<EmailSettings> emailSettings, IHttpContextAccessor httpContextAccessor)
        {
            _emailSettings = emailSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_emailSettings.Email),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mail.To.Add(new MailAddress(email));

            using var smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSsl,
                UseDefaultCredentials = _emailSettings.UseDefaultCredentials
            };

            await smtp.SendMailAsync(mail);
        }

        public async Task SendVerificationEmailAsync(string email, string token)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var request = httpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var confirmationLink = $"{baseUrl}/Account/ConfirmEmail?email={WebUtility.UrlEncode(email)}&token={WebUtility.UrlEncode(token)}";

            string subject = "Confirm your email";
            string message = $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; }}
                        .container {{ padding: 20px; max-width: 600px; margin: 0 auto; }}
                        .button {{ background-color: #4CAF50; border: none; color: white; padding: 15px 32px; text-align: center; text-decoration: none; display: inline-block; font-size: 16px; margin: 4px 2px; cursor: pointer; border-radius: 4px; }}
                        .footer {{ margin-top: 30px; font-size: 12px; color: #888; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>Email Verification</h2>
                        <p>Thank you for registering with our hotel application. Please click the button below to verify your email address:</p>
                        <p><a href='{confirmationLink}' class='button'>Verify Email</a></p>
                        <p>If you did not register on our website, you can safely ignore this email.</p>
                        <p>If you're having trouble clicking the button, copy and paste the URL below into your web browser:</p>
                        <p>{confirmationLink}</p>
                        <div class='footer'>
                            <p>This is an automated message, please do not reply to this email.</p>
                        </div>
                    </div>
                </body>
                </html>";

            await SendEmailAsync(email, subject, message);
        }
    }
}
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class EmailSenderService : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailSender = "MS_TVbzOX@trial-yzkq340rv064d796.mlsender.net";
            var password = "DpkIFFKou8MN9lai"; // Retrieve from secure storage

            var msg = new MailMessage();
            msg.From = new MailAddress(emailSender);
            msg.Subject = subject;
            msg.To.Add(email);
            msg.Body = htmlMessage;
            msg.IsBodyHtml = true;

            using (var smtpClient = new SmtpClient("smtp.mailersend.net")
            {
                UseDefaultCredentials = false,
                Port = 587, // Use 465 for SSL, 587 for TLS
                Credentials = new NetworkCredential(emailSender, password),
                EnableSsl = true
            })
            {
                try
                {
                    await smtpClient.SendMailAsync(msg);
                }
                catch (SmtpException ex)
                {
                    // Log the exception or handle it as needed
                    throw new InvalidOperationException("Could not send email", ex);
                }
            }
        }
    }
}

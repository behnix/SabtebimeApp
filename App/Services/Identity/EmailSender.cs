using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace App.Services.Identity
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var siteEmail = "info@sabtebime.com";
            var siteEmailPassword = "Pooyan@1803";
            var siteSmtp = "mail.sabtebime.com";
            var siteSmtpPort = 25;
            var message = new MailMessage(siteEmail, email, subject, htmlMessage)
            {
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8
            };

            var smtpClient = new SmtpClient(siteSmtp, siteSmtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(siteEmail, siteEmailPassword),
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            return smtpClient.SendMailAsync(message);
        }
    }
}
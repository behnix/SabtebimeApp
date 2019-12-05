using System.Net.Mail;

namespace App.Core.Senders
{   
    public class SendEmail
    {
        public static void Send(string to, string subject, string body, string siteSmtp, string siteEmail, string siteNameInEnglish, int smtpPort, string siteEmailPassword)
        {
            var mail = new MailMessage();
            var smtpServer = new SmtpClient(siteSmtp);
            mail.From = new MailAddress(siteEmail, siteNameInEnglish);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            smtpServer.Port = smtpPort;
            smtpServer.Credentials = new System.Net.NetworkCredential(siteEmail, siteEmailPassword);
            smtpServer.EnableSsl = smtpPort != 25;

            smtpServer.Send(mail);

        }
    }
}
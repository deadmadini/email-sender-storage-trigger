using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace EmailSenderStorageTrigger
{
    internal static class EmailSender
    {
        public static void SendSASToken(string link, string receiver)
        {
            string sender = Environment.GetEnvironmentVariable("SenderMail");
            string senderPassword = Environment.GetEnvironmentVariable("SenderMailPassword");

            MailMessage message = new MailMessage();
            message.From = new MailAddress(sender);
            message.Subject = "Link to the uploaded file";
            message.To.Add(receiver);
            message.Body = "<html><body>" +
                "<h2>Link to the upploaded file<h2>" +
                $"Your file was successully uploaded to the BLOB storage. To get access to your file, use the next link: <br>{link}." +
                $"This link is available for only one hour." +
                "</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(sender, senderPassword),
                EnableSsl = true
            };

            smtpClient.Send(message);
        }
    }
}

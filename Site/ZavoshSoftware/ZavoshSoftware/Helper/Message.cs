using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Helper
{
    public class Message
    {
        public void Send(List<string> recievers, string message,string messageType)
        {
            if (messageType == "email")
                SendEmail(recievers, message);
        }

        public void SendEmail(List<string> recievers, string message)
        {
            SmtpClient client = new SmtpClient("webmail.zavoshsoftware.com");
            //If you need to authenticate
            client.Credentials = new NetworkCredential("support@zavoshsoftware.com", "Tj59a@egFbg3");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("support@zavoshsoftware.com");
            mailMessage.To.Add("babaei.aho@gmail.com");
            mailMessage.Subject = "Hello There";
            mailMessage.Body = "Hello my friend!";
            mailMessage.IsBodyHtml = true;

            client.Send(mailMessage);
        }
    }
}
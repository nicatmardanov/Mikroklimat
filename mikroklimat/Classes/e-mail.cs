using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace mikroklimat.Classes
{
    public class e_mail
    {
        public e_mail(Models.user us)
        {
            Classes.Forgot_Password fp = new Classes.Forgot_Password(Classes.Crypto.Decrypt(us.password));
            MailMessage message = new MailMessage();
            message.From = new MailAddress("mikroklimat@outlook.com.tr");
            message.To.Add(new MailAddress(Classes.Crypto.Decrypt(us.email)));
            message.Subject = Classes.Forgot_Password.subject;
            message.IsBodyHtml = true;
            message.Body = fp.body;


            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.live.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("mikroklimat@outlook.com.tr", "C3l1lD4d4s0v");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            smtp.Dispose();
        }
    }
}
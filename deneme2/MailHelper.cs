using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace deneme2
{
    public class MailHelper
    {

        public static bool SendMail(string adres, string konu, string mesaj)
        {
            bool dondur = false;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("Deneme1783@gmail.com");
                mail.To.Add(adres);
                mail.Subject = konu;
                mail.Body = mesaj;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("Deneme1783@gmail.com", "123456789Rg");
                SmtpServer.EnableSsl = true;   
               // SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Send(mail);               
                dondur = true;
                
            }
            catch(Exception ex)
            {
                dondur = false;
                
            }

            return dondur;

        }

    }
}

using System;
using System.Configuration;

using System.Threading.Tasks;
//using System.Web.Script.Serialization;
using System.Net.Mail;

namespace PetGroomingApplication.Services
{
    public class Email
    {
 
        public static void Send(string email, string subject, string body)
        {
            if (String.IsNullOrEmpty(email))
                return;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["ContactEmailAddress"]);
                mail.Subject = subject;

                mail.Body = body;

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["SmtpHost"];
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpAccount"], ConfigurationManager.AppSettings["SmtpPass"]); // ***use valid credentials***
                smtp.Port = Int32.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw(new Exception("Exception in sendEmail:" + ex.Message));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace ChampService.Utils
{
    public class EmailHandler
    {
      public static bool SendEmail(string toAddr, string mailSubject, string mailBody)
            {
                // read mail params from config
                string userID = ConfigurationManager.AppSettings["UserName"].ToString();
                string passWord = ConfigurationManager.AppSettings["Password"].ToString();
                string fromAddr = ConfigurationManager.AppSettings["EmailId"].ToString();
                string mailServer = ConfigurationManager.AppSettings["MailServer"].ToString();
                int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString());
                MailMessage message = new MailMessage(fromAddr, toAddr);

              
                message.Subject = mailSubject;
                message.Body = mailBody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
  
                SmtpClient client = new SmtpClient(mailServer, smtpPort); //smtp port, for gmail use 587
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(userID, passWord);

                try
                {
                    client.Send(message);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return true;
            }


        public static bool SendattachmentEmail(string toAddr, string mailSubject, string mailBody)
        {
            // read mail params from config
            string userID = ConfigurationManager.AppSettings["UserName"].ToString();
            string passWord = ConfigurationManager.AppSettings["Password"].ToString();
            string fromAddr = ConfigurationManager.AppSettings["EmailId"].ToString();
            string mailServer = ConfigurationManager.AppSettings["MailServer"].ToString();
            int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString());
            MailMessage message = new MailMessage(fromAddr, toAddr);

            System.Net.Mail.Attachment attachment;
            //P:\API_WS\MediaUpload\Images\Java Spring Boot Developer.pdf.pdf
            //attachment = new System.Net.Mail.Attachment("P:\\API_WS\\MediaUpload\\Images\\"+ "Java Spring Boot Developer.pdf");
            //message.Attachments.Add(attachment);

            message.Subject = mailSubject;
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient(mailServer, smtpPort); //smtp port, for gmail use 587
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(userID, passWord);

            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

    }
}

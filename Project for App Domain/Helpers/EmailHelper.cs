using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project_for_App_Domain.Models;
using System.Net.Mail;
using System.Configuration;

namespace Project_for_App_Domain.Helpers
{
    public class EmailHelper
    {
        public void SendEmail(EmailModel em)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    // Setup the SMTP client info
                    client.Host = ConfigurationManager.AppSettings["SMTPServer"];
                    client.Port = Int32.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    var username = ConfigurationManager.AppSettings["SMTPUsername"];
                    if (!string.IsNullOrWhiteSpace(username))
                    {
                        System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(username, ConfigurationManager.AppSettings["SMTPPassword"]);
                        client.Credentials = SMTPUserInfo;
                    }
                    else
                    {
                        client.UseDefaultCredentials = true;
                    }

                    // Create the message
                    MailMessage mail = new MailMessage();
                    //mail.Attachments.Add(new System.Net.Mail.Attachment(@"C:\Program Files\SAPToSharePoint\Images\Image1.jpg") { ContentId = "Image1" });
                    //mail.Attachments.Add(new System.Net.Mail.Attachment(@"C:\Program Files\SAPToSharePoint\Images\Image2.jpg") { ContentId = "Image2" });
                    //mail.Attachments.Add(new System.Net.Mail.Attachment(@"C:\Program Files\SAPToSharePoint\Images\Image3.jpg") { ContentId = "Image3" });
                    //mail.Attachments.Add(new System.Net.Mail.Attachment(@"C:\Program Files\SAPToSharePoint\Images\PortalOverview.pdf"));
                    mail.To.Add(em.SendTo);
                    mail.From = new MailAddress(em.SendFrom);
                    mail.Subject = em.Subject;
                    mail.Body = em.EmailBody;

                    // Send and log message success
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
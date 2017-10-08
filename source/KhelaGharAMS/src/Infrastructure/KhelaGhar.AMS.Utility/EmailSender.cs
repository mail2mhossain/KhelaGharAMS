using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Utility
{
    public class EmailSender
    {
        private const string Password = "f8psf2Ku";
        private const string SmtpAddress = "smartmeter-noreply@sinepulse.net";
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromAddress { get; set; }
        public string[] ToList { get; set; }
        public string[] CcList { get; set; }
        public string[] BccList { get; set; }
        public Attachment[] Files { get; set; }

        public bool Sendmail()
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(FromAddress);

            if (this.ToList != null)
            {
                foreach (string toadd in this.ToList)
                {
                    if (toadd.Trim().Length > 0)
                    {
                        mailMessage.To.Add(new MailAddress(toadd));
                    }
                }
            }
            if (this.CcList != null)
            {
                foreach (string cc in this.CcList)
                {
                    if (cc.Trim().Length > 0)
                    {
                        mailMessage.CC.Add(cc);
                    }
                }
            }
            //Set additional options
            mailMessage.Priority = MailPriority.High;
            //Text/HTML
            mailMessage.IsBodyHtml = true;

            mailMessage.Subject = this.Subject;
            mailMessage.Body = this.Body;

            if (this.Files != null)
            {
                foreach (Attachment filename in this.Files)
                {
                    mailMessage.Attachments.Add(filename);
                }
            }
            SmtpClient smtpClient = new SmtpClient("smtpout.secureserver.net", 25);
            smtpClient.Credentials = new NetworkCredential(SmtpAddress, Password);
            smtpClient.EnableSsl = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtpClient.Send(mailMessage);
                mailMessage.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Attachment GetAttachment(string filename)
        {
            Attachment attachment = new Attachment(filename);

            return attachment;
        }
    }
}

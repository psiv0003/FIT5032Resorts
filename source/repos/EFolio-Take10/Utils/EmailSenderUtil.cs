using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;

namespace EFolio_Take10.Utils
{
    public class EmailSenderUtil
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents, HttpPostedFileBase emailAttachment)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@localhost.com", "FIT5032 Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            byte[] fileInBytes = new byte[emailAttachment.ContentLength];
            using (BinaryReader theReader = new BinaryReader(emailAttachment.InputStream))
            {
                fileInBytes = theReader.ReadBytes(emailAttachment.ContentLength);
            }
            string fileAsString = Convert.ToBase64String(fileInBytes);
            msg.AddAttachment(emailAttachment.FileName, fileAsString);
            var response = client.SendEmailAsync(msg);
        }

    }
}

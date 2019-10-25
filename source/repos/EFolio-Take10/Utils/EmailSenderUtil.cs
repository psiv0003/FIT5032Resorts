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
            var from = new EmailAddress("noreply@relic.com", "Relic Resorts");
            String[] emailList = toEmail.Split(',');
            List<EmailAddress> toList = new List<EmailAddress>();
            for (int i = 0; i < emailList.Length; i++)
            {
                toList.Add(new EmailAddress(emailList[i], ""));
            }
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, toList, subject, plainTextContent, htmlContent);
            if (emailAttachment != null)
            {
                byte[] fileInBytes = new byte[emailAttachment.ContentLength];
                using (BinaryReader theReader = new BinaryReader(emailAttachment.InputStream))
                {
                    fileInBytes = theReader.ReadBytes(emailAttachment.ContentLength);
                }
                string fileAsString = Convert.ToBase64String(fileInBytes);
                msg.AddAttachment(emailAttachment.FileName, fileAsString);
            }
            var response = client.SendEmailAsync(msg);
        }
        //public void Send(string toemail, string subject, string contents, httppostedfilebase emailattachment)
        //{
        //    var client = new sendgridclient(api_key);
        //    var from = new emailaddress("noreply@localhost.com", "fit5032 email user");
        //    var to = new emailaddress(toemail, "");
        //    var plaintextcontent = contents;
        //    var htmlcontent = "<p>" + contents + "</p>";
        //    var msg = mailhelper.createsingleemail(from, to, subject, plaintextcontent, htmlcontent);
        //    if (emailattachment != null)
        //    {
        //        byte[] fileinbytes = new byte[emailattachment.contentlength];
        //        using (binaryreader thereader = new binaryreader(emailattachment.inputstream))
        //        {
        //            fileinbytes = thereader.readbytes(emailattachment.contentlength);
        //        }
        //        string fileasstring = convert.tobase64string(fileinbytes);
        //        msg.addattachment(emailattachment.filename, fileasstring);
        //    }
        //    client.sendemailasync(msg);

        //byte[] fileinbytes = new byte[emailattachment.contentlength];
        //using (binaryreader thereader = new binaryreader(emailattachment.inputstream))
        //{
        //    fileinbytes = thereader.readbytes(emailattachment.contentlength);
        //}
        //string fileasstring = convert.tobase64string(fileinbytes);
        //msg.addattachment(emailattachment.filename, fileasstring);
        // }

        //public void Send(String toEmail, String subject, String contents, HttpPostedFileBase emailAttachment)
        //{
        //    var client = new SendGridClient(API_KEY);
        //    var from = new EmailAddress("noreply@localhost.com", "FIT5032 Email User");
        //    //    String[] emailList = toEmail.Split(','); 
        //    //    List<EmailAddress> toList = new List<EmailAddress>();
        //    //    for (int i = 0; i < emailList.Length; i++)
        //    //    {
        //    //        toList.Add(new EmailAddress(emailList[i], ""));
        //    //    }
        //    var to = new EmailAddress(toEmail, "");
        //    var plainTextContent = contents;
        //    var htmlContent = "<p>" + contents + "</p>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    if (emailAttachment != null)
        //    {
        //        //byte[] fileinbytes = new byte[emailAttachment.contentlength];
        //        //using (binaryreader thereader = new binaryreader(emailAttachment.inputstream))
        //        //{
        //        //    fileinbytes = thereader.readbytes(emailAttachment.contentlength);
        //        //}
        //        //string fileasstring = convert.tobase64string(fileinbytes);
        //        //msg.emailAttachment(emailAttachment.filename, fileasstring);
        //        //byte[] fileinbytes = new byte[emailattachment.contentlength];
        //        using (binaryreader thereader = new binaryreader(emailAttachment.inputstream))
        //        {
        //            fileinbytes = thereader.readbytes(emailAttachment.contentlength);
        //        }
        //        string fileasstring = convert.tobase64string(emailAttachment);
        //        msg.emailAttachment(emailAttachment.filename, fileasstring);
        //    }

        //    var response = client.SendEmailAsync(msg);
        //}
        //public void Send(String toEmail, String subject, String contents)
        //{
        //    var client = new SendGridClient(API_KEY);
        //    var from = new EmailAddress("noreply@localhost.com", "FIT5032 Example Email User");
        //    var to = new EmailAddress(toEmail, "");
        //    var plainTextContent = contents;
        //    var htmlContent = "<p>" + contents + "</p>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = client.SendEmailAsync(msg);
        //}

    }
}

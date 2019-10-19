using EFolio_Take10.Models;
using EFolio_Take10.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFolio_Take10.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Send_Email()


        {

            return View(new EmailSender());
        }
        public ActionResult Index()
        {
            //EmailSender es = new EmailSender();
            //es.RegisterAPIKey();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Send_Email(EmailSender model, HttpPostedFileBase emailAttachment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSenderUtil es = new EmailSenderUtil();
                    es.Send(toEmail, subject, contents, emailAttachment);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new EmailSender());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

    }
}
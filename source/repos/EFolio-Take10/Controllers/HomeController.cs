using EFolio_Take10.Models;
using EFolio_Take10.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFolio_Take10.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ResortEntities db = new ResortEntities();


        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

      

        public ActionResult Send_Email()

        {


            //var users = db.AspNetUsers.ToList();
            //List<AspNetUser> userList = users.ToList();
            //List<IEnumerable> list = new List<IEnumerable>();
            //foreach (AspNetUser i in userList)
            //{

            //    list.Add(i.Email);
            //}
            //MultiSelectList emailDropDown = new MultiSelectList(list);
            //var p = new EmailSender
            //{
            //    DropDownList = emailDropDown
            //};
            //var dependent = db.AspNetUsers.ToList();

            //ViewBag.app_key = new MultiSelectList(db.AspNetUsers, "Id", "email", list);

            //ViewBag.UserList = new MultiSelectList(userList, "Email", "Email");

            var result = db.AspNetUsers.ToList();
            if (result != null)
            {
                ViewBag.mySkills = result.Select(N => new SelectListItem { Text = N.Email, Value = N.Email.ToString() });
            }

          //  ViewBag.data = new AspNetUser().Email();

            return View();
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
        public ActionResult Send_Email( EmailSender model, HttpPostedFileBase emailAttachment)
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

                    return View();
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
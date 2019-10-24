using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EFolio_Take10.Models
{
    public class EmailSender
    {
        //[Display(Name = "Email address")]
        //[Required(ErrorMessage = "Please enter an email address.")]
        public string ToEmail { get; set; }

        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the contents")]
        [AllowHtml]
        public string Contents { get; set; }

        public HttpPostedFileBase Upload { get; set; }
        public List<string> SelectedEmails { get; set; }
        public MultiSelectList DropDownList { get; set; }
    }

   
}
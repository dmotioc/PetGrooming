using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetGroomingApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            if ( User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Admin");

            }
            else if (User.IsInRole("staff"))
            {
                string calendarDate = DateTime.Today.ToString("yyyy-MM-dd"); 
                return RedirectToAction("Calendar", "Groomer", new { date = calendarDate } );
            }            
            else if (User.IsInRole("staff"))
            {
                return RedirectToAction("Admin");
            }

            else if (User.IsInRole("user"))
            {
                return RedirectToAction("CreateAppointment", "Owner");
            }


            string galleryPath = "~/Content/images/gallery";
            Dictionary<string, IEnumerable<string>> galleryFiles = new Dictionary<string, IEnumerable<string>>();
            foreach (Species species in Enum.GetValues(typeof(Species))) {
                string[] files;
                try
                {
                    files = Directory.GetFiles(Server.MapPath(galleryPath + "/" + species.ToString()))
                        .Select(x => Path.GetFileName(x))
                        .ToArray();
                    galleryFiles.Add(species.ToString() , files);
                }
                catch
                { }
             }
            ViewBag.gallery = galleryFiles;


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // POST: /Home/Contact
        [HttpPost]
        public JsonResult Contact(ContactViewModel contact)
        {
            string ourEmail = "dana.motioc@gmail.com";
            //ContactViewModel contact = new ContactViewModel();
            //UpdateModel(contact);
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                PetGroomingApplication.Services.Email.sendEmail(
                ourEmail,
                String.Format($"{contact.Subject} - from {contact.Name} ({contact.From})"),
                contact.Message);
                result["success-message"] = "Your message was sent sussessfully";
            }
            catch(Exception ex)
            {
                result["error-message"] = ex.Message;
            }
            return Json(result);
       }

    }
}
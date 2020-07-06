using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Configuration;
using System.Threading.Tasks;
using PetGroomingApplication.Models;
using PetGroomingApplication.GenericRepository;
using PetGroomingApplication.Repository;
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

            //else if (User.IsInRole("user"))
            //{
            //    return RedirectToAction("CreateAppointment", "Owner");
            //}

             Dictionary<string, string> dogServices = new Dictionary<string, string>
             {
                 { "Full Grooming Service", "A bath with towel and hair drying, teeth brushing, nail trimming, eye and ear cleaning, a brush out, and a haircut based upon your dog’s breed standard or your individual style choice."},
                 { "Bath Grooming", "A bath, trimmed nails, and cleaned ears."},
                 { "De-matting", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ip sum has been the industry's standard dummy text ever."}
             };
             ViewBag.dogServices = dogServices;

             ViewBag.catServices = new Dictionary<string, string>
             {
                 { "Full Grooming Service" , "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ip sum has been the industry's standard dummy text ever."},
                 { "Bath Grooming", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ip sum has been the industry's standard dummy text ever."},
                 { "De-matting", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ip sum has been the industry's standard dummy text ever." }
             };

            GroomerRepository groomerRepo = new GroomerRepository();
            IEnumerable<Groomer> groomers = groomerRepo.GetAll();
            var groomerList = new List<Dictionary<string,  string>>();
            string groomerPath = "/Content/images/groomers";
            foreach (Groomer groomer in groomers)
            {
                groomerList.Add(new Dictionary<string, string> {
                    {"name", groomer.Name },
                    {"specializing", groomer.Specializing.ToString()},
                    {"file" , groomerPath + "/" + groomer.Name + ".jpg" }
                });
            }
            ViewBag.groomers = groomerList;

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

            ViewBag.mapKey = ConfigurationManager.AppSettings["GoogleMapKey"];



            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // POST: /Home/Contact
        [HttpPost]
        public ActionResult Contact(ContactViewModel contact)
        {
            string ourEmail = System.Configuration.ConfigurationManager.AppSettings["ContactEmailAddress"];
            //ContactViewModel contact = new ContactViewModel();
            //UpdateModel(contact);
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                PetGroomingApplication.Services.Email.Send(
                ourEmail,
                String.Format($"Contact subject: {contact.Subject} - from {contact.Name} ({contact.From})"),
                contact.Message);
                result["success-message"] = "The message was sent successfully.";
            }
            catch(Exception ex)
            {
                result["error-message"] = ex.Message;
            }
            return Json(result);
       }

    }
}
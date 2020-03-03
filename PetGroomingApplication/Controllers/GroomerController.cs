using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PetGroomingApplication.GenericRepository;
using PetGroomingApplication.Models;
using PetGroomingApplication.Repository;
using PetGroomingApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PetGroomingApplication.Controllers
{
    public class GroomerController : Controller
    {
        private GroomerRepository repository = null;
        private AppointmentRepository appointmentRepository = null;
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
 
        public GroomerController()
        {
            this.repository = new GroomerRepository();
            this.appointmentRepository = new AppointmentRepository();
        }

        public GroomerController(GroomerRepository repository)
        {
            this.repository = repository;
        }

        // GET: Groomer/Calendar?date=date
        [Authorize(Roles = "staff")]
        public ActionResult Calendar(DateTime date)
        {
            string userId = User.Identity.GetUserId();
            Groomer groomer =  repository.GetByUserId(userId);
            List<Appointment> appointments = appointmentRepository.GetAppointmentsByGroomerByDate(groomer.GroomerID, date);
            TimeSpan startTime = groomer.StartWorkTime.TimeOfDay;
            TimeSpan endTime = groomer.EndWorkTime.TimeOfDay;

            List<GroomerCalendarViewModel> calendar = CalendarService.GroomerAppointmentsCalendar(
                appointments, startTime, endTime
            );
        
            ViewBag.Date = date.ToString("dd-MM-yyyy");
            ViewBag.groomerName = groomer.Name;
            return View("Appointments", calendar);
        }

        // GET: Groomer
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var groomers = repository.GetAll();
            return View("Index", groomers);
        }

        // GET: Groomer/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: Groomer/Register
        [Authorize(Roles = "admin")]
        public ActionResult Register()
        {
            return View("Register");
        }

        // POST: Owner/Create with user
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult> Register(GroomerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                model.Password = RandomPasswordService.GenerateRandomPassword(); // random password
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "staff");
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    //create groomer
                    Groomer groomer = new Groomer();
                    UpdateModel(groomer);
                    groomer.UserId = user.Id;
                    repository.Insert(groomer);
                    repository.Save();
                    return RedirectToAction("SuccessRegister", new {email = model.Email, pass= model.Password });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //GET: Groomer/Success register
        public ActionResult SuccessRegister(string email, string pass)
        {
            
            // send email to groomer with user & password
            // ...........................           
           
            // show authenticate data, only for demo
            ViewBag.Email = email;
            ViewBag.Pass = pass;
            return View("SuccessRegister");
        }


        // GET: Groomer/Edit/5
        [Authorize(Roles = "staff, admin")]
        public ActionResult Edit(Guid id)
        {
            Groomer groomer = repository.GetById(id);
            return View("Edit", groomer);
        }

        // POST: Groomer/Edit/5
        [HttpPost]
        [Authorize(Roles = "staff, admin")]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            Groomer groomer = new Groomer();
            try
            {
                UpdateModel(groomer);
                repository.Update(groomer);
                repository.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit", groomer);
            }
        }

        // GET: Groomer/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(Guid id)
        {
            Groomer groomer = repository.GetById(id);
            return View("Delete", groomer);
        }

         // POST: Groomer/Delete/5
        [HttpPost]
        [Authorize(Roles ="admin")]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                repository.Delete(id);
                repository.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}

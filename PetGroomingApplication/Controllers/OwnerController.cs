using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
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
    public class OwnerController : Controller
    {
        private OwnerRepository ownerRepository = null;
        private GroomerRepository groomerRepository = null;
        private ServiceRepository serviceRepository = null;
        private PetRepository petRepository = null;
        private AppointmentRepository appointmentRepository = null;
        private const int CALENDAR_SIZE_IN_DAYS = 7;

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
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
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public OwnerController()
        {
            this.ownerRepository = new OwnerRepository();
            this.groomerRepository = new GroomerRepository();
            this.serviceRepository = new ServiceRepository();
            this.petRepository = new PetRepository();
            this.appointmentRepository = new AppointmentRepository();
        }

        //  ------------     Actions    -------------    //                                    
        
        #region public Actions    
 
        // GET: Owner (all appointments by owner)
        public ActionResult Index()
        {
            Guid ownerID = GetIDByUserId();
            List<Guid> myPets = petRepository.GetByOwner(ownerID).Select(x => x.PetID).ToList();
            List<Appointment> myAppointments = appointmentRepository.GetAppointmentsByPetList(myPets);
            return View("Appointments", myAppointments);
        }

        // GET: Owner/Register
        public ActionResult Register()
        {
            return View("Register");
        }

        // POST: Owner/Create with user
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Register(OwnerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "User");
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    //create owner
                    Owner owner = new Owner();
                    UpdateModel(owner);
                    owner.UserId = user.Id;
                    ownerRepository.Insert(owner);
                    ownerRepository.Save();
                    return RedirectToAction("CreateAppointment", "Owner");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: Owner/Edit
        [Authorize(Roles = "user")]
        public ActionResult Edit()
        {
            Guid ownerID = GetIDByUserId();
            Owner owner = ownerRepository.GetById(ownerID);
            return View(owner);
        }

        // POST: Owner/Edit
        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult Edit(FormCollection collection)
        {
            Owner owner = new Owner();
            owner.UserId = User.Identity.GetUserId(); ;
            try
            {
                UpdateModel(owner);
                ownerRepository.Update(owner);
                ownerRepository.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(owner);
            }
        }

        // GET: /Owner/CreateAppointment
        [Authorize(Roles = "user")]
        public ActionResult CreateAppointment()
        {
            Guid ownerID = GetIDByUserId();
            Appointment appointment = Session["Appointment"] as Appointment ?? new Appointment();
            CreateAppointmentListInitializer(appointment, ownerID);
            return View("CreateAppointment", appointment);
        }

        // Ajax POST: /Owner/GetGroomersAndServices
        [HttpPost]
        [Authorize(Roles = "user")]
        public PartialViewResult GetGroomersAndServices(string id)
        {
            Appointment appointment = Session["Appointment"] as Appointment ?? new Appointment();
            if (!String.IsNullOrEmpty(id))
            {
                Guid petID = Guid.Parse(id);
                setServicesAndGroomerSelectListByPet(petID);

                appointment.PetID = petID;
                Session["Appointment"] = appointment;
            }
            return PartialView("GetGroomersAndServicesPartial", appointment);
        }

        // Ajax POST: /Owner/ServiceDetails
        [HttpPost]
        [Authorize(Roles = "user")]
        public PartialViewResult ServiceDetails(string id)
        {
            Appointment appointment = Session["Appointment"] as Appointment ?? new Appointment();
            if (!String.IsNullOrEmpty(id))
            {
                Guid serviceID = Guid.Parse(id);
                appointment.ServiceID = serviceID;
                appointment.Service = serviceRepository.GetById(serviceID);
                Session["Appointment"] = appointment;
            }
            return PartialView("GetServiceDetailsPartial", appointment);

        }

        // Ajax POST: /Owner/GroomerCalendar
        [HttpPost]
        [Authorize(Roles = "user")]
        public PartialViewResult GroomerCalendar(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                Guid groomerID = Guid.Parse(id);
                Groomer groomer = groomerRepository.GetById(groomerID);
                ViewBag.groomerName = groomer.Name;
                Appointment appointment = Session["Appointment"] as Appointment ?? new Appointment();
                appointment.GroomerID = groomerID;
                appointment.Groomer = groomer;
                Session["Appointment"] = appointment;
                ViewBag.calendar = GetAvailabilityCalendarView(groomer);
                return PartialView("GetGroomerCalendarPartial");
            }
            return default;
        }

        // Ajax Get: /Owner/CheckAvailability
        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult CheckAvailability(Appointment appointment)
        {
            if (appointment.Service == null)
            {
                appointment.Service = serviceRepository.GetById(appointment.ServiceID);
                Session["Appointment"] = appointment;
            }
            if (appointment.Groomer == null)
            {
                appointment.Groomer = groomerRepository.GetById(appointment.GroomerID);
            }
            bool output = CreateAppointmentService.IsAppointmentPossible(appointment);
            return Content(output.ToString().ToLower());
        }

        // Ajax POST: /Owner/CreateAppointment
        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult CreateAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                if (appointment.Service == null)
                {
                    appointment.Service = serviceRepository.GetById(appointment.ServiceID);
                }
                if (appointment.Groomer == null)
                {
                    appointment.Groomer = groomerRepository.GetById(appointment.GroomerID);
                }

                if (!CreateAppointmentService.IsAppointmentPossible(appointment))
                {
                       return Content("There is not enough available time for this service at this time!");
                }
                //    save appointment 
                try
                {
                    appointmentRepository.SaveAppointment(appointment);
                    Session["Appointment"] = null;
                    return Content("OK");
                }
                catch(Exception e)
                {
                    return Content("Save appointment error: " + e.Message + "!");
                }
            }
            //ModelState.AddModelError("", "Invalid input!");
            return Content("All fields are required!");
        }

        public ActionResult AppointmentRegisterSuccess() {
            return View("AppointmentRegisterSuccess");
        }
        
        //public ActionResult GroomerCalendar_TEST()
        //{
        //    Guid groomerID = new Guid("92C665AD-A54D-EA11-BF15-083E8EBA6E02");
        //    Groomer groomer = groomerRepository.GetById(groomerID);
        //    var date1 = new DateTime(2020, 2, 13);
        //    var date2 = new DateTime(2020, 2, 14);

        //    var calendar = new Dictionary<DateTime, List<AvailabilityCalendarViewModel>>() {
        //        {date1,  GetGroomerCalendarByDay(date1, groomer)},
        //        {date2,  GetGroomerCalendarByDay(date2, groomer)},
        //    };
        //    //var calendar = GetGroomerCalendarByDay(date, groomer);
        //    //ViewBag.Date = date.Date;
        //    ViewBag.groomerName = groomer.Name;
        //    ViewBag.calendar = calendar;
        //    return View("GetGroomerCalendarPartial_TEST", calendar);
        //}

        // GET: Owner/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: Owner/Delete/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        //  -----------     privates    -------------   //

        #region private helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private Guid GetIDByUserId()
        {
            string userId = User.Identity.GetUserId();
            return ownerRepository.GetIdByUserId(userId);
        }
        private void CreateAppointmentListInitializer(Appointment appointment, Guid ownerID)
        {
            List<Pet> pets = petRepository.GetByOwner(ownerID);
            SelectList lstPets = new SelectList(pets, "PetID", "Name");
            ViewData["myPets"] = lstPets;

            if (appointment.PetID != Guid.Empty)
            {
                setServicesAndGroomerSelectListByPet(appointment.PetID);
            }
            else
            {
                setServicesAndGroomerSelectListEmpty();
            }

            if (appointment.GroomerID != Guid.Empty) { 
                Groomer groomer = groomerRepository.GetById(appointment.GroomerID);
                ViewBag.calendar = GetAvailabilityCalendarView(groomer);
            }

        }
        private void setServicesAndGroomerSelectListByPet(Guid petID)
        {
            Species species = petRepository.GetById(petID).Species;
            List<Service> services = serviceRepository.GetBySpecies(species);
            List<Groomer> groomers = groomerRepository.GetBySpecialization(species);
            SelectList lstServices = new SelectList(services, "ServiceID", "Name");
            SelectList lstGroomers = new SelectList(groomers, "GroomerID", "Name");
            ViewData["services"] = lstServices;
            ViewData["groomers"] = lstGroomers;
        }
        private void setServicesAndGroomerSelectListEmpty()
        {
            List<Service> services = new List<Service>();
            List<Groomer> groomers = new List<Groomer>();
            SelectList lstServices = new SelectList(services, "ServiceID", "Name");
            SelectList lstGroomers = new SelectList(groomers, "GroomerID", "Name");
            ViewData["services"] = lstServices;
            ViewData["groomers"] = lstGroomers;
        }

        private Dictionary<DateTime, List<AvailabilityCalendarViewModel>> GetAvailabilityCalendarView(Groomer groomer)
        {

            // calendar for CALENDAR_SIZE_IN_DAYS days , start from today
             return CreateAppointmentService.GetCalendarViewByGroomerByDateInterval(groomer, DateTime.Today.Date, CALENDAR_SIZE_IN_DAYS);
            
        }

        #endregion

    }
}

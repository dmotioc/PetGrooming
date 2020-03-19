using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PetGroomingApplication.GenericRepository;
using PetGroomingApplication.Models;
using PetGroomingApplication.Repository;
using PetGroomingApplication.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
            TimeSpan startTime = groomer.StartWorkingTime.TimeOfDay;
            TimeSpan endTime = groomer.EndWorkingTime.TimeOfDay;

            List<GroomerCalendarViewModel> calendar = CalendarService.GroomerAppointmentsCalendar(
                appointments, startTime, endTime
            );
        
            ViewBag.Date = date.ToString("dd.MM.yyyy");
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
            GroomerRegisterViewModel groomer = new  GroomerRegisterViewModel();
            return View("Register", groomer);
        }

        // POST: Groomer/Register with user
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult> Register(GroomerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Name,
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

                    // send email to groomer with user & password
                     
                    string subject = "Pet Spa credentials";
                    ViewBag.UserName = model.Name;
                    ViewBag.Password = model.Password;
                    string message = RenderRazorViewToString("RegisterEmail", null);
                    string emailResult;
                    try
                    {
                        PetGroomingApplication.Services.Email.sendEmail(model.Email, subject, message);
                        emailResult = "The message was sent sussessfully";
                    }
                    catch (Exception ex)
                    {
                        emailResult = ex.Message;
                        // write on disk
                        string fileName = Path.GetFullPath(Server.MapPath(@"~/Data/Sent_credentials.txt"));

                        using (StreamWriter file = new StreamWriter(fileName, true))
                        {
                            file.WriteLine($"{DateTime.Now}\nUser name: {model.Name} \nPassword: {model.Password}\n-------------------");
                        }
                    }
                    ViewBag.EmailResult = emailResult;
                    return RedirectToAction("SuccessRegister");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //GET: Groomer/Success register
        public ActionResult SuccessRegister(string email, string pass)
        {
            
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
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
            Groomer groomer = repository.GetById(id);
            try
            {
                string userId = repository.GetById(id).UserId;
                repository.Delete(id);
                repository.Save();

                 ApplicationUser user = await UserManager.FindByIdAsync(userId);
                if(user != null)
                {
                    var result = await UserManager.DeleteAsync(user);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Cannot delete user!");
                        AddErrors(result);
                        return View("Delete");
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                if (e.GetBaseException().GetType() == typeof(SqlException))
                {
                    Int32 ErrorCode = ((SqlException)e.GetBaseException()).Number;
                    switch (ErrorCode)
                    {
                        case 2627:  // Unique constraint error
                            break;
                        case 547:   // Constraint check violation
                            ModelState.AddModelError("", "There are appointments linked to this groomer!");
                            break;
                        case 2601:  // Duplicated key row error
                            break;
                        default:
                            ModelState.AddModelError("", e.Message);
                            break;
                    }
                }
               
                ModelState.AddModelError("", "The groomer cannot be deleted!");

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


        private string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}

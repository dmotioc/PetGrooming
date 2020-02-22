using PetGroomingApplication.GenericRepository;
using PetGroomingApplication.Repository;
using PetGroomingApplication.Models;
using PetGroomingApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace PetGroomingApplication.Controllers
{
    public class AppointmentController : Controller
    {
        private AppointmentRepository appointmentRepository = null;
        private GroomerRepository groomerRepository = null;
        private IGenericRepository<Service> serviceRepository = null;
        private IGenericRepository<Pet> petRepository = null;

        public AppointmentController()
        {
            this.appointmentRepository = new AppointmentRepository();
            this.groomerRepository = new GroomerRepository();
            this.serviceRepository = new GenericRepository<Service>();
            this.petRepository = new GenericRepository<Pet>();
        }

        // GET: Appointment
        [Authorize(Roles = "admin, staff")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Appointment/Details/5
        [Authorize(Roles = "admin, staff")]
        public ActionResult Details(Guid id)
        {
            Appointment appointment = appointmentRepository.GetById(id);
            return Content("Ok");
        }
        
        [HttpGet]
        [Route("appointment/groomer")]
        [Authorize(Roles = "staff")]
        public ActionResult GroomerAppointments(DateTime date)
        {
            // List<Appointment> appointments = appointmentRepository.GetAppointmentsByGroomerByDate(id, date.Date);
            string userId = User.Identity.GetUserId();
            Guid groomerID = groomerRepository.GetIdByUserId(userId);
            CalendarService calendar = new CalendarService();
            List<CalendarViewModel> appointmentsCalendar = calendar.GetAppointmentsCallendarByGroomerByDate(groomerID, date.Date);
            ViewBag.Date = date.Date;
            ViewBag.groomerID = groomerID;
            ViewBag.groomerName = groomerRepository.GetById(ViewBag.groomerID = groomerID).Name;
            return View("GroomerAppointments", appointmentsCalendar);
        }
        
        public ActionResult ListByCustomer(Guid id)
        {
            return View();
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: Appointment/Delete/5
        [HttpPost]
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
    }
}

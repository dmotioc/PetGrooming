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
        private IGenericRepository<Owner> ownerRepository = null;
        public AppointmentController()
        {
            this.appointmentRepository = new AppointmentRepository();
            this.groomerRepository = new GroomerRepository();
            this.serviceRepository = new GenericRepository<Service>();
            this.petRepository = new GenericRepository<Pet>();
            this.ownerRepository = new GenericRepository<Owner>();
        }

        // GET: Appointment
        [Authorize(Roles = "admin, staff")]
        public ActionResult Index(DateTime date)
        {
            List<Appointment> appointments= appointmentRepository.GetAppointmentsByDate(date);
            ViewBag.Date = date.ToString("dd.MM.yyyy");
            return View(appointments);
        }

        // GET: Appointment/Details/5
        [Authorize(Roles = "admin, staff")]
        public ActionResult Details(Guid id)
        {
            Appointment appointment = appointmentRepository.GetById(id);
            return View("Details", appointment);
        }


        // GET: Appointment/Delete/5
        [Authorize(Roles = "admin, staff")]
        public ActionResult Delete(Guid id)
        {
            Appointment appointment = appointmentRepository.GetById(id); 
            return View("Delete", appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost]
        [Authorize(Roles = "admin, staff")]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            Appointment appointment = appointmentRepository.GetById(id);
            try
            {
                DateTime date = appointment.DateTime;
                appointmentRepository.Delete(id);
                appointmentRepository.Save();
                return RedirectToAction("Index", new { date = date.ToString("yyyy-MM-dd") });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error at delete appointment: " + e.Message);
                return View("Delete", appointment);
            }
        }
    }
}

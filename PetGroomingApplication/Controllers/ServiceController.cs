using PetGroomingApplication.GenericRepository;
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetGroomingApplication.Controllers
{
    public class ServiceController : Controller
    {
        private IGenericRepository<Service> repository = null;

        public ServiceController()
        {
            this.repository = new GenericRepository<Service>();
        }

        public ServiceController(GenericRepository<Service> repository)
        {
            this.repository = repository;
        }
        // GET: Services
        public ActionResult Index()
        {
            var services = repository.GetAll();
            return View(services);
        }

        // GET: Services/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Services/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Service service = new Service();
                UpdateModel(service);
                repository.Insert(service);
                repository.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Services/Edit/5
        public ActionResult Edit(Guid id)
        {
            Service service = repository.GetById(id);
            return View("Edit" , service);
        }

        // POST: Services/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            Service service = new Service(); 
            try
            {
                string rate = collection["Rate"];
                //collection["Rate"] = decimal.Parse(rate, System.Globalization.NumberStyles.Currency);
                UpdateModel(service);
                repository.Update(service);
                repository.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Services/Delete/5
        public ActionResult Delete(Guid id)
        {
            Service service = repository.GetById(id);
            return View("Delete", service);
        }

        // POST: Services/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                repository.Delete(id);
                repository.Save();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                int errorCode;
                if (int.TryParse(e.Message, out errorCode) && errorCode == (int)DbError.ConstraintCheckViolation)
                {
                    ModelState.AddModelError("", "This service has registered appointments. You have to cancel related appointments in order to delete it!");
                }
                else
                {
                    ModelState.AddModelError("", e.Message);
                }
                Service service = repository.GetById(id);
                return View("Delete", service);
            }
        }
    }
}

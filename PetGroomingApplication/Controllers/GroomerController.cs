using PetGroomingApplication.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetGroomingApplication.DAL;

namespace PetGroomingApplication.Controllers
{
    public class GroomerController : Controller
    {
        private IGenericRepository<Groomer> repository = null;

        public GroomerController()
        {
            this.repository = new GenericRepository<Groomer>();
        }

        public GroomerController(IGenericRepository<Groomer> repository)
        {
            this.repository = repository;
        }

        // GET: Groomer
        public ActionResult Index()
        {
            var groomerModel = repository.GetAll();
            return View(groomerModel);
        }

        // GET: Groomer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Groomer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groomer/Create
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

        // GET: Groomer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Groomer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: Groomer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Groomer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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

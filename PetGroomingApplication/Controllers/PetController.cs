using PetGroomingApplication.GenericRepository;
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetGroomingApplication.Controllers
{
    public class PetController : Controller
    {
        private IGenericRepository<Pet> repository = null;

        public PetController()
        {
            this.repository = new GenericRepository<Pet>();
        }

        public PetController(GenericRepository<Pet> repository)
        {
            this.repository = repository;
        }
        // GET: Pet
        public ActionResult Index()
        {
            var pets = repository.GetAll();
            return View("Index", pets);
        }

        // GET: Pet/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: Pet/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Pet/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Pet pet = new Pet();
                UpdateModel(pet);
                repository.Insert(pet);
                repository.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Pet/Edit/5
        public ActionResult Edit(Guid id)
        {
            Pet pet = repository.GetById(id);
            return View("Edit", pet);
        }

        // POST: Pet/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            Pet pet = new Pet();
            try
            {
                UpdateModel(pet);
                repository.Update(pet);
                repository.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit", pet);
            }
        }

        // GET: Pet/Delete/5
        public ActionResult Delete(Guid id)
        {
            Pet pet = repository.GetById(id);
            return View("Delete", pet);
        }

        // POST: Pet/Delete/5
        [HttpPost]
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
    }
}

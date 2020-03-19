using Microsoft.AspNet.Identity;
using PetGroomingApplication.GenericRepository;
using PetGroomingApplication.Models;
using PetGroomingApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetGroomingApplication.Controllers
{
    public class PetController : Controller
    {
        private PetRepository repository = null;
        private OwnerRepository ownerRepository = null;
        private Guid ownerID = Guid.Empty;

        public PetController()
        {
            this.repository = new PetRepository();
            this.ownerRepository = new OwnerRepository();
        }

        //public PetController(PetRepository repository)
        //{
        //    this.repository = repository;
        //}

        // GET: Pet
        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            ownerID = ownerRepository.GetIdByUserId(userId);
            var pets = repository.GetByOwner(ownerID);
            return View("Index", pets);
        }

        // GET: Pet/Create
        [Authorize(Roles = "user")]
        public ActionResult Create()
        {
            ViewBag.returnUrl = Request.UrlReferrer.AbsolutePath;
            return View("Create");
        }

         // POST: Pet/Create
         [HttpPost]
         [Authorize(Roles = "user")]
         public ActionResult Create(Pet pet, string returnUrl)
         {
            try
            {
                string userId = User.Identity.GetUserId();
                ownerID = ownerRepository.GetIdByUserId(userId); 
                pet.OwnerID = ownerID;
                repository.Insert(pet);
                repository.Save();
                return Redirect(returnUrl);
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Pet/Edit/5
        [Authorize(Roles = "user")]
        public ActionResult Edit(Guid id)
        {
            Pet pet = repository.GetById(id);
            return View("Edit", pet);
        }

         // POST: Pet/Edit/5
         [HttpPost]
         [Authorize(Roles = "user")]
         public ActionResult Edit(Guid id, FormCollection collection)
         {
            Pet pet = new Pet();
            try
            {
                UpdateModel(pet);
                string userId = User.Identity.GetUserId();
                ownerID = ownerRepository.GetIdByUserId(userId);
                pet.OwnerID = ownerID;
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
        [Authorize(Roles = "user")]
        public ActionResult Delete(Guid id)
        {
            Pet pet = repository.GetById(id);
            return View("Delete", pet);
        }

        // POST: Pet/Delete/5
        [HttpPost]
        [Authorize(Roles = "user")]
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

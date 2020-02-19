using Microsoft.AspNet.Identity.Owin;
using PetGroomingApplication.GenericRepository;
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetGroomingApplication.Controllers
{
    public class GroomerController : Controller
    {
        private IGenericRepository<Groomer> repository = null;
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
            this.repository = new GenericRepository<Groomer>();
        }

        public GroomerController(GenericRepository<Groomer> repository)
        {
            this.repository = repository;
        }

        // GET: Groomer
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

        // GET: Groomer/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Groomer/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = model.Email,
        //            Email = model.Email
        //            //FullName = model.FullName,
        //            //Contact = model.Contact
        //        };
        //        var result = await UserManager.CreateAsync(user, model.Password);





                public ActionResult Create(FormCollection collection)
        {
            try
            {
                var registeredUser = UserManager.FindByNameAsync(User.Identity.Name);
 
                Groomer groomer = new Groomer();
                UpdateModel(groomer);
                //groomer.UserId =  registeredUser.Id;
                repository.Insert(groomer);
                repository.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Groomer/Edit/5
        public ActionResult Edit(Guid id)
        {

            Groomer groomer = repository.GetById(id);
            return View("Edit", groomer);
        }

        // POST: Groomer/Edit/5
        [HttpPost]
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
        public ActionResult Delete(Guid id)
        {
            Groomer groomer = repository.GetById(id);
            return View("Delete", groomer);
        }

        // POST: Groomer/Delete/5
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

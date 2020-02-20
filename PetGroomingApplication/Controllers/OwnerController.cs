using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PetGroomingApplication.GenericRepository;
using PetGroomingApplication.Models;
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
        private IGenericRepository<Owner> repository = null;
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
            this.repository = new GenericRepository<Owner>();
        }


        // GET: Owner
        public ActionResult Index()
        {
            return View();
        }

        // GET: Owner/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: Owner/Create
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
                    repository.Insert(owner);
                    repository.Save();
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: Owner/Edit/5
        public ActionResult Edit(Guid id)
        {
            Owner owner = repository.GetById(id);
            return View(owner);
        }

        // POST: Owner/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
                Owner owner = new Owner();
           try
            {
                UpdateModel(owner);
                repository.Update(owner);
                repository.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(owner);
            }
        }

        // GET: Owner/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: Owner/Delete/5
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}

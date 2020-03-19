using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetGroomingApplication.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestWriter()
        {
            string fileName = Path.GetFullPath(Server.MapPath(@"~/Data/Sent_credentials.txt"));


            using (StreamWriter file = new StreamWriter(fileName, true))
            {
                file.WriteLine($"{DateTime.Now}\nUser name: xxx \nPassword: yyy\n-------------------");
            }
            return   new  EmptyResult();
        }
    }
}
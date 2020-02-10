using PetGroomingApplication.DAL;
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Repository
{
    public class GroomerRepository
    {
        //private Models.DBObjects.PetGroomingModelsDataContext dbContext;
        private GroomingContext groomingContext;

        public GroomerRepository()
        {
            this.groomingContext = new GroomingContext();
        }
        public List<Groomer> GetAllGroomers()
        {
            
            return groomingContext.Groomers.ToList();
        }




    }
}
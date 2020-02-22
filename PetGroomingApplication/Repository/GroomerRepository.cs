using PetGroomingApplication.DAL;
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Repository
{
    public class GroomerRepository: PetGroomingApplication.GenericRepository.GenericRepository<Groomer>
    {
        //private Models.DBObjects.PetGroomingModelsDataContext dbContext;
        private GroomingContext groomingContext;

        public GroomerRepository()
        {
            this.groomingContext = new GroomingContext();

        }
        public Guid GetIdByUserId(string userId)
        {
            this.groomingContext.Database.Log += s => Debug.WriteLine(s);
            Guid groomerID = groomingContext.Groomers
                .Where(g => g.UserId == userId)
                .Select(g => g.GroomerID)
                .FirstOrDefault();
            return groomerID;
        }
    }
}
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
        public Groomer GetByUserId(string userId)
        {
            //this.groomingContext.Database.Log += s => Debug.WriteLine(s);
            Groomer groomer = groomingContext.Groomers
                .Where(g => g.UserId == userId)
                .FirstOrDefault();
            return groomer;
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
        public List<Groomer> GetBySpecialization(Species species)
        {
            this.groomingContext.Database.Log += s => Debug.WriteLine(s);
            List<Groomer> groomers = groomingContext.Groomers
                .Where(e => e.Specializing == species)
                .ToList();
            return groomers;
        }


    }
}
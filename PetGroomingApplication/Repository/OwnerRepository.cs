using PetGroomingApplication.DAL;
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Repository
{
    public class OwnerRepository: PetGroomingApplication.GenericRepository.GenericRepository<Owner>
    {
        //private Models.DBObjects.PetGroomingModelsDataContext dbContext;
        private GroomingContext groomingContext;

        public OwnerRepository()
        {
            this.groomingContext = new GroomingContext();

        }
        public Owner GetByUserId(string userId)
        {
            this.groomingContext.Database.Log += s => Debug.WriteLine(s);
            Owner owner = groomingContext.Owners
                .Where(g => g.UserId == userId)
                .FirstOrDefault();
            return owner;
        }

        public Guid GetIdByUserId(string userId)
        {
            this.groomingContext.Database.Log += s => Debug.WriteLine(s);
            Guid ownerID = groomingContext.Owners
                .Where(g => g.UserId == userId)
                .Select(g => g.OwnerID)
                .FirstOrDefault();
            return ownerID;
        }
    }
}
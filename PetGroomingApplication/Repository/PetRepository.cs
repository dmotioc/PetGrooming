using PetGroomingApplication.DAL;
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Repository
{
    public class PetRepository: PetGroomingApplication.GenericRepository.GenericRepository<Pet>
    {
         private GroomingContext groomingContext;

        public PetRepository()
        {
            this.groomingContext = new GroomingContext();

        }

        public List<Pet> GetByOwner(Guid ownerID)
        {
            this.groomingContext.Database.Log += s => Debug.WriteLine(s);
            List<Pet> pets = groomingContext.Pets
                .Where(e => e.OwnerID == ownerID)
                .ToList();
            return pets;
        }


    }
}
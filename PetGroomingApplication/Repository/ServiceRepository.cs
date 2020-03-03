using PetGroomingApplication.DAL;
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Repository
{
    public class ServiceRepository: PetGroomingApplication.GenericRepository.GenericRepository<Service>
    {
        //private Models.DBObjects.PetGroomingModelsDataContext dbContext;
        private GroomingContext groomingContext;

        public ServiceRepository()
        {
            this.groomingContext = new GroomingContext();

        }
        public List<Service> GetBySpecies(Species species)
        {
            this.groomingContext.Database.Log += s => Debug.WriteLine(s);
            List<Service> services = groomingContext.Services
                .Where(e => e.Species == species)
                .ToList();
            return services;
        }
        public int GetServiceLength(Guid serviceID)
        {
            int serviceLength = groomingContext.Services
                 .Where(e => e.ServiceID == serviceID)
                 .Select(e => e.DurationInMinutes)
                 .First();

            return serviceLength;

        }
    }
}
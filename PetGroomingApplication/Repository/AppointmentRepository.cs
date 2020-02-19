using PetGroomingApplication.DAL;
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Repository
{
    public class AppointmentRepository: PetGroomingApplication.GenericRepository.GenericRepository<Appointment>
    {
        //private Models.DBObjects.PetGroomingModelsDataContext dbContext;
        private GroomingContext groomingContext;

        public AppointmentRepository()
        {
            this.groomingContext = new GroomingContext();

        }
        public List<Appointment> GetAppointmentsByGroomerByDate(Guid groomerID, DateTime date)
        {
            this.groomingContext.Database.Log += s => Debug.WriteLine(s);
            var appointments = groomingContext.Appointments
                        .Where(e => e.GroomerID == groomerID)
                        .Where(e => System.Data.Entity.DbFunctions.TruncateTime(e.DateTime) == date)
                        .OrderBy(e => e.DateTime)
                        .ToList();
            return appointments;
        }
    }
}
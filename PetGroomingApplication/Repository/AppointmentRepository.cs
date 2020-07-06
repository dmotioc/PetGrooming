using PetGroomingApplication.DAL;
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Repository
{
    public class AppointmentRepository : PetGroomingApplication.GenericRepository.GenericRepository<Appointment>
    {
        //private Models.DBObjects.PetGroomingModelsDataContext dbContext;
        private GroomingContext groomingContext;

        public AppointmentRepository()
        {
            this.groomingContext = new GroomingContext();

        }
        public List<Appointment> GetAppointmentsByPetList(List<Guid> pets)
        {
            this.groomingContext.Database.Log += s => Debug.WriteLine(s);

            var appointments = groomingContext.Appointments
                       .Where(a => pets.Contains(a.PetID))
                       .OrderByDescending(e => e.DateTime)
                       .ToList();
            return appointments;
        }

        public List<Appointment> GetAppointmentsByGroomerByDate(Guid groomerID, DateTime date)
        {
            this.groomingContext.Database.Log += s => Debug.WriteLine(s);
            date = date.Date;
            var appointments = groomingContext.Appointments
                        .Where(e => e.GroomerID == groomerID)
                        .Where(e => System.Data.Entity.DbFunctions.TruncateTime(e.DateTime) == date)
                        .OrderBy(e => e.DateTime)
                        .ToList();
            return appointments;
        }

        public List<Appointment> GetAppointmentsByDate(DateTime date)
        {
            this.groomingContext.Database.Log += s => Debug.WriteLine(s);
            date = date.Date;
            var appointments = groomingContext.Appointments
                        .Where(e => System.Data.Entity.DbFunctions.TruncateTime(e.DateTime) == date)
                        .OrderBy(e => e.GroomerID)
                        .ToList();
            return appointments;
        }

        public void SaveAppointment(Appointment appointment)
        {
           appointment.Groomer = null;
           appointment.Service = null;
           appointment.Pet = null;
           Insert(appointment); // inherit from GenericRepository
           Save();
        }
    }
}
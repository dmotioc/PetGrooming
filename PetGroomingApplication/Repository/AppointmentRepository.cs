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
                                //         .Where(e => e.GroomerID == groomerID && Convert.ToDateTime(e.DateTime).Date === date)
                                .Where(e => System.Data.Entity.DbFunctions.TruncateTime(e.DateTime) == date)
                        .OrderBy(e => e.DateTime)
                        .ToList();
            return appointments;
        }
    }
}
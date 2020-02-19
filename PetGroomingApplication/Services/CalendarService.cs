using PetGroomingApplication.Models;
using PetGroomingApplication.Repository;
using PetGroomingApplication.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Services
{
    public class CalendarService
    {
        private AppointmentRepository appointmentRepository = null;
        private IGenericRepository<Groomer> groomerRepository = null;
        private TimeSpan timeStep = TimeSpan.FromMinutes(30);


        public CalendarService()
        {
            this.appointmentRepository = new AppointmentRepository();
            this.groomerRepository = new GenericRepository<Groomer>();
        }

        public List<CalendarViewModel> GetAppointmentsCallendarByGroomerByDate(
            Guid groomerID,
            DateTime date
        )
        {
            List<CalendarViewModel> calendar = new List<CalendarViewModel>();
            List<Appointment> appointments = appointmentRepository.GetAppointmentsByGroomerByDate(groomerID, date);
            Groomer groomer = groomerRepository.GetById(groomerID);
            TimeSpan begining = groomer.StartWorkTime.TimeOfDay;
            TimeSpan end = groomer.EndWorkTime.TimeOfDay;

            TimeSpan index = begining;
            foreach (Appointment appointment in appointments)
            {
                while (appointment.DateTime.TimeOfDay > index)
                {
                    calendar.Add(createAvailableCalendarUnit(index));
                    index += timeStep;
                }
                TimeSpan appointmentLength = TimeSpan.FromMinutes(appointment.Service.DurationInMinutes);
                while (appointment.DateTime.TimeOfDay + appointmentLength > index)
                {
                    calendar.Add(createAppointmentCalendarUnit(index, appointment));
                    index += timeStep;
                }
            }
            while (end > index)
            {
                calendar.Add(createAvailableCalendarUnit(index));
                index += timeStep;
            }
            return calendar;
        }

        private CalendarViewModel createAvailableCalendarUnit(TimeSpan time)
        {
            return new CalendarViewModel
            {
                Time = time,
                Status = Status.Available,
                AppointmentID = null
            };
        }

        private CalendarViewModel createAppointmentCalendarUnit(TimeSpan time, Appointment appointment)
        {

            //if (appointment == null)
            //{
            //    return null;
            //}
            //else
            //{
            CalendarViewModel model = new CalendarViewModel
            {
                Time = time,
                Status = Status.Booked,
                IsAppointmentStart = (appointment.DateTime.TimeOfDay == time),
                AppointmentID = appointment.AppointmentID,
                AppointmentShortDetails = new List<string>
                {
                    appointment.Service.Name,
                    string.Format("{0}, {1} years", appointment.Pet.Breed, appointment.Pet.Age.ToString())
                }

            };
            return model;
            // }
        }
    }
}
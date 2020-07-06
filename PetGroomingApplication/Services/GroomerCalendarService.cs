using PetGroomingApplication.Models;
using PetGroomingApplication.Repository;
using System;
using System.Collections.Generic;

namespace PetGroomingApplication.Services
{
    public class GroomerCalendarService
    {
        private AppointmentRepository appointmentRepository = null;
        private readonly Groomer groomer = null;
        private readonly static TimeSpan timeStep = TimeSpan.FromMinutes(30);

        public GroomerCalendarService(Groomer groomer)
        {
            this.appointmentRepository = new AppointmentRepository();
            this.groomer = groomer;
        }

        public List<CalendarModel> GetCalendar(DateTime date)
        {
            List<Appointment> appointments = appointmentRepository.GetAppointmentsByGroomerByDate(groomer.GroomerID, date);
            TimeSpan startTime = groomer.StartWorkingTime.TimeOfDay;
            TimeSpan endTime = groomer.EndWorkingTime.TimeOfDay;

            List<CalendarModel> calendar = AppointmentsToCalendar(
                appointments, startTime, endTime
            );

            return calendar;
        }
        public static TimeSpan getTimeStep()
        {
            return timeStep;
        }

        private static List<CalendarModel> AppointmentsToCalendar(
    List<Appointment> appointments, TimeSpan begining, TimeSpan end
)
        {
            /// param:   List<Appointments> for 1 day, 1 groomer, order DateTime ascending
            /// returns: List<CalendarViewModel> = time units and coresponding appointments || null

            List<CalendarModel> calendar = new List<CalendarModel>();

            TimeSpan index = begining;
            foreach (Appointment appointment in appointments)
            {
                while (appointment.DateTime.TimeOfDay > index)
                {
                    calendar.Add(CalendarUnit(index));
                    index += timeStep;
                }
                TimeSpan appointmentLength = TimeSpan.FromMinutes(appointment.Service.DurationInMinutes);
                while (appointment.DateTime.TimeOfDay + appointmentLength > index)
                {
                    calendar.Add(CalendarUnit(index, appointment));
                    index += timeStep;
                }
            }
            while (end > index)
            {
                calendar.Add(CalendarUnit(index));
                index += timeStep;
            }
            return calendar;
        }
        private static CalendarModel CalendarUnit(TimeSpan time)
        {
            return CalendarUnit(time, null);
        }
        private static CalendarModel CalendarUnit(TimeSpan time, Appointment appointment)
        {
            return new CalendarModel
            {
                Time = time,
                Appointment = appointment
            };
        }

    }
}
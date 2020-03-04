using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Services
{


    public static class CalendarService
    {
        private readonly static TimeSpan timeStep = TimeSpan.FromMinutes(30);

        public static DateTime[] DateRange(DateTime dateStart, int dayCount) { 
            return Enumerable.Range(0, dayCount)
           .Select(offset => dateStart.AddDays(offset))
           .ToArray();
        }
         public static List<AvailabilityCalendarViewModel> AvailabilityCalendar(
             List<Appointment> appointments, TimeSpan begining, TimeSpan end
        )
        {
            return CalendarToAvailabilityCalendarMapper(
                GetAppointmentsCalendar(appointments, begining, end)
            );
        }

        public static List<GroomerCalendarViewModel> GroomerAppointmentsCalendar(
              List<Appointment> appointments, TimeSpan begining, TimeSpan end
         )
        {
            return CalendarToGroomerCalendarMapper(
                GetAppointmentsCalendar(appointments, begining, end)
            );
        }

        public static List<CalendarModel> GetAppointmentsCalendar(
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
                    calendar.Add(calendarUnit(index));
                    index += timeStep;
                }
                TimeSpan appointmentLength = TimeSpan.FromMinutes(appointment.Service.DurationInMinutes);
                while (appointment.DateTime.TimeOfDay + appointmentLength > index)
                {
                    calendar.Add(calendarUnit(index, appointment));
                    index += timeStep;
                }
            }
            while (end > index)
            {
                calendar.Add(calendarUnit(index));
                index += timeStep;
            }
            return calendar;
        }
        private static CalendarModel calendarUnit(TimeSpan time)
        {
            return calendarUnit(time, null);
        }
        private static CalendarModel calendarUnit(TimeSpan time, Appointment appointment)
        {
            return new CalendarModel
            {
                Time = time,
                Appointment = appointment
            };
        }

        public static bool IsAppointmentPosible(Appointment appointment, int serviceLength, List<AvailabilityCalendarViewModel> calendar)
        {
            ///  check if datetime and length of appointment is in groomer working interval &&
            ///  is not overlapping with a booked period            
            bool isPosible = true;
            TimeSpan appointmentStart = appointment.DateTime.TimeOfDay;
            TimeSpan appointmentLength = TimeSpan.FromMinutes(serviceLength);

            if (appointmentStart < calendar.Select(x => x.Time).FirstOrDefault())
            {
                return false;
            }
            if (appointmentStart+ appointmentLength > calendar.Select(x => x.Time).LastOrDefault() + timeStep)
            {
                return false;
            }

            foreach (AvailabilityCalendarViewModel calendarItem in calendar
                .Where(x => x.Time >= appointmentStart
                && x.Time < appointmentStart + appointmentLength))
            {
                if (calendarItem.Status == Status.Booked )
                {
                    isPosible = false;
                    return isPosible;
                }
            }
            return isPosible;
        }

        private static List<GroomerCalendarViewModel> CalendarToGroomerCalendarMapper(List<CalendarModel> calendar)
        {
            List<GroomerCalendarViewModel> groomerCalendar = new List<GroomerCalendarViewModel>();
            foreach (CalendarModel calendarItem in calendar)
            {
                if (calendarItem.Appointment != null)
                {
                    groomerCalendar.Add(new GroomerCalendarViewModel
                    {
                        Time = calendarItem.Time,
                        Status = Status.Booked,
                        IsAppointmentStart = (calendarItem.Appointment.DateTime.TimeOfDay == calendarItem.Time),
                        AppointmentID = calendarItem.Appointment.AppointmentID,
                        AppointmentShortDetails = new List<string>
                        {
                            calendarItem.Appointment.Service.Name,
                            string.Format("{0}, {1} years", calendarItem.Appointment.Pet.Breed, calendarItem.Appointment.Pet.Age.ToString())
                        }
                    });
                }
                else
                {
                    groomerCalendar.Add(new GroomerCalendarViewModel
                    {
                        Time = calendarItem.Time,
                        Status = Status.Available
                    });

                }
            }
            return groomerCalendar;
        }

        private static List<AvailabilityCalendarViewModel> CalendarToAvailabilityCalendarMapper(List<CalendarModel> calendar)
        {
            List<AvailabilityCalendarViewModel> ownerCalendar = new List<AvailabilityCalendarViewModel>();
            foreach (CalendarModel calendarItem in calendar)
            {
                ownerCalendar.Add(new AvailabilityCalendarViewModel
                {
                    Time = calendarItem.Time,
                    Status = (calendarItem.Appointment != null ? Status.Booked : Status.Available)
                });
            }
            return ownerCalendar;
        }


    }
}
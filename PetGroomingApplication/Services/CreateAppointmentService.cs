using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Services
{
    public static class CreateAppointmentService
    {


        public static Dictionary<DateTime, List<AvailabilityCalendarViewModel>>  GetCalendarViewByGroomerByDateInterval(Groomer groomer, DateTime dateInit, int days) { 

            var calendarView = new Dictionary<DateTime, List<AvailabilityCalendarViewModel>>();
            GroomerCalendarService groomerService = new GroomerCalendarService(groomer);
            foreach (DateTime date in DateRange(dateInit, days))
            {
                calendarView.Add(date, CalendarViewMapperService.CalendarToCreateAppointmentView(groomerService.GetCalendar(date)));
            };
            return calendarView;
        }
        public static bool IsAppointmentPossible(Appointment appointment)
        {

            ///  if datetime and length of appointment is in groomer working interval 
            /// && is not overlapping with a booked period            
            bool isPosible = true;
            TimeSpan appointmentStart = appointment.DateTime.TimeOfDay;
            TimeSpan appointmentLength = TimeSpan.FromMinutes(appointment.Service.DurationInMinutes);
            Groomer groomer = appointment.Groomer;
            GroomerCalendarService groomerService = new GroomerCalendarService(groomer);
            List<CalendarModel> calendar = groomerService.GetCalendar(appointment.DateTime.Date);

            //CalendarModel = 
            if (appointmentStart < calendar.Select(x => x.Time).FirstOrDefault())
            {
                return false;
            }
            if (appointmentStart + appointmentLength > calendar.Select(x => x.Time).LastOrDefault() + GroomerCalendarService.getTimeStep())
            {
                return false;
            }

            foreach (CalendarModel calendarItem in calendar
                .Where(x => x.Time >= appointmentStart
                && x.Time < appointmentStart + appointmentLength))
            {
                if (calendarItem.Appointment != null )
                {
                    isPosible = false;
                    return isPosible;
                }
            }
            return isPosible;
        }

         public static DateTime[] DateRange(DateTime dateStart, int dayCount)
        {
            return Enumerable.Range(0, dayCount)
           .Select(offset => dateStart.AddDays(offset))
           .ToArray();
        }


    }
}
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Services
{
    public class CalendarViewMapperService
    {

         //  calendar in groomer view format
        public static List<GroomerCalendarViewModel> CalendarToGroomerView(List<CalendarModel> calendar)
        {
            
            List<GroomerCalendarViewModel> calendarView = new List<GroomerCalendarViewModel>();
            foreach (CalendarModel calendarItem in calendar)
            {
                if (calendarItem.Appointment != null)
                {
                    calendarView.Add(new GroomerCalendarViewModel
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
                    calendarView.Add(new GroomerCalendarViewModel
                    {
                        Time = calendarItem.Time,
                        Status = Status.Available
                    });

                }
            }
            return calendarView;
        }

        // calendar in create appointment format
        public static List<AvailabilityCalendarViewModel> CalendarToCreateAppointmentView(List<CalendarModel> calendar)
        {
            List<AvailabilityCalendarViewModel> calendarView = new List<AvailabilityCalendarViewModel>();
            foreach (CalendarModel calendarItem in calendar)
            {
                calendarView.Add(new AvailabilityCalendarViewModel
                {
                    Time = calendarItem.Time,
                    Status = (calendarItem.Appointment != null ? Status.Booked : Status.Available)
                });
            }
            return calendarView;
        }

    }
}
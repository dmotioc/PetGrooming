using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Models
{
    public enum Status
    {
        OffDuty = 0,
        Available = 1,
        Booked = 2,
    }

    public class GroomerCalendarViewModel
    {
        public TimeSpan Time { get; set; }
        public Status Status { get; set; }
        public bool IsAppointmentStart { get; set; }
        public Guid? AppointmentID { get; set; }
        public List<string> AppointmentShortDetails { get; set; }
    }
}
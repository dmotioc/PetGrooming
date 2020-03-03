using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Models
{
    public class CalendarModel
    {
        public TimeSpan Time { get; set; }
        public Appointment Appointment { get; set; }
     }
}
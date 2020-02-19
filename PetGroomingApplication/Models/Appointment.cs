using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AppointmentID { get; set; }
        
        [Required]
        [ForeignKey("Service")]
        public Guid ServiceID { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        //[Required]
        // public int DurationInMinutes { get; set; }

        [Required]
        [ForeignKey("Groomer")]
        public Guid GroomerID { get; set; }

        [Required]
        [ForeignKey("Pet")]
        public Guid PetID { get; set; }
        public virtual Service Service { get; set; }
        public virtual Groomer Groomer { get; set; }
        public virtual Pet Pet { get; set; }

    }
}
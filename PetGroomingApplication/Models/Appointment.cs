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
        [Display(Name = "Service")]
        public Guid ServiceID { get; set; }

        [Required]
        [Display(Name = "Date and time")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)] 
        public DateTime DateTime { get; set; }

        [Required]
        [ForeignKey("Groomer")]
        [Display(Name = "Groomer")]
        public Guid GroomerID { get; set; }

        [Required]
        [ForeignKey("Pet")]
        [Display(Name = "Pet")] 
        public Guid PetID { get; set; }
        public virtual Service Service { get; set; }
        public virtual Groomer Groomer { get; set; }
        public virtual Pet Pet { get; set; }

    }
}
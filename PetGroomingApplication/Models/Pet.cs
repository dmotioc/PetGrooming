using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Models
{
    public class Pet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PetID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Species Species { get; set; }
        
        [Required]
        public string Breed { get; set; }
        
        [Required]
        public int Age { get; set; }

        [Display(Name = "Somthing else we should know")]
        [StringLength(1000)]
        public string Comments { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual Owner Owner { get; set; }

    }
}
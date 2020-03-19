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
        [ForeignKey("Owner")]
        public Guid OwnerID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Please select the species")]
        public Species Species { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public int Age { get; set; }

        [Display(Name = "Other details")]
        [StringLength(1000)]
        public string Comments { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual Owner Owner { get; set; }

    }
}
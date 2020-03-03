using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid ServiceID { get; set; }
        
        [Required]
        public Species Species { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "String too long (max. 50 chars)")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Duration (minutes)")]
        public int DurationInMinutes {get; set; }
        
        [Display(Name = "Rate")]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
 
    }

 }
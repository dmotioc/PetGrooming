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
        private decimal _rate;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid ServiceID { get; set; }
        
        [Required]
        [Range(1, 1000, ErrorMessage = "Please select the species")]
        public Species Species { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "String too long (max. 50 chars)")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Duration (minutes)")]
        public int DurationInMinutes {get; set; }
        
        [Display(Name = "Rate")]

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Rate {
            get { return _rate; }
            set { _rate = value; }
        }
        public virtual ICollection<Appointment> Appointments { get; set; }
 
    }

 }
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PetGroomingApplication.Models;

namespace PetGroomingApplication.Models
{
    public class Groomer
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GroomerID  { get; set; }
        [Required]
        public  string Name { get; set; }
        
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Working Time")]
        public DateTime StartWorkTime { get; set; }
        
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Working Time")] 
        public DateTime EndWorkTime { get; set; }
        
        [Required]
        [Display(Name = "Specializing (Species)")]
        public Species Specializing { get; set; }
 
        public int UserId { get; set; }
        
        public virtual ICollection<Appointment> Appointments { get; set; }



    }
}
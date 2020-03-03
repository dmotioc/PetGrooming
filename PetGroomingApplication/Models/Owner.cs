using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Models
{
    public class Owner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OwnerID { get; set; }
        
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }
       
        [Required]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        
        [Display(Name ="Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string  UserId { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }

     }
}
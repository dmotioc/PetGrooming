using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Models
{
    public class OwnerRegisterViewModel
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

        public int UserId { get; set; }

        //  Identity props

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //  navigation props
        public virtual ICollection<Pet> Pets { get; set; }


    }
}
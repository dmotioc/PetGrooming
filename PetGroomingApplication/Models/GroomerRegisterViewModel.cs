using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PetGroomingApplication.Models;

namespace PetGroomingApplication.Models
{
    public class GroomerRegisterViewModel
    {
        
        private readonly static DateTime DEFAULT_START_TIME = DateTime.Now.Date + new TimeSpan(9, 0 ,0);
        private readonly static DateTime DEFAULT_END_TIME = DateTime.Now.Date + new TimeSpan(17, 0, 0);
        private DateTime _startWorkingTime;
        private DateTime _endWorkingTime;


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GroomerID  { get; set; }
       
        [Required]
        public  string Name { get; set; }
        
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Working Time")]
        public DateTime StartWorkingTime {
            get {
                if (_startWorkingTime == DateTime.MinValue)
                    return DEFAULT_START_TIME;
                return _startWorkingTime;
            }
            set { _startWorkingTime = value;  }
        }
        
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Working Time")] 
        public DateTime EndWorkingTime {
            get
            {
                if (_endWorkingTime == DateTime.MinValue)
                    return DEFAULT_END_TIME;
                return _endWorkingTime;
            }
            set { _endWorkingTime = value; }
        }

        [Required]
        [Range(1, 1000, ErrorMessage = "Please select the specialization")]
        [Display(Name = "Specializing (Species)")]
        public Species Specializing { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }




    }
}
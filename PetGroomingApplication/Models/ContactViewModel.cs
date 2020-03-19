using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Models
{
    public class ContactViewModel
    {
        [Required]
        public string Name {get; set;}
        
        [Required]
        public string From { get; set; }
        
        [Required] 
        public string Subject { get; set; }
        
        [Required] 
        public string Message { get; set; }

    }
}
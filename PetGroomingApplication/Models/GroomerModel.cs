using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Models
{
    public class GroomerModel
    {
        public Guid ID  { get; set; }
        public  string Name { get; set; }
        public DateTime StartWorkTime { get; set; }
        public DateTime EndWorkTime { get; set; }
        public int ExpertiseLevel { get; set; }

    }
}
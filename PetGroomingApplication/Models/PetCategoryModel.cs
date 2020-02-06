using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.Models
{
    public class PetCategoryModel
    {
        public Guid IDPetCategory { get; set; }
        public Categories Category { get; set; }
        public Sizes Size { get; set; }
        public SpeciesE Species { get; set; }

    }

    public enum Categories
    {
        NotSpecified =0,
        LongHair = 1, 
        MediumHair =2, 
        ShortHair =3
    }
    public enum Sizes
    {
        NotSpecified = 0,
        Small = 1,
        Medium = 2,
        Big = 3
    }
    public enum SpeciesE
    {
        NotSpecified = 0,
        Dog = 1,
        Cat = 2,
        Rabbit = 3
    }

}
using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.DAL
{
    public class GroomingInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<GroomingContext>
    {
        protected override void Seed(GroomingContext context)
        {
            var groomers = new List<GroomerModel>
            {
                new GroomerModel { ID = new Guid(), Name = "Carson", StartWorkTime = DateTime.Parse("2005-09-01 08:00"), EndWorkTime = DateTime.Parse("2005-09-01 16:00"), ExpertiseLevel = 3 }
            };
            groomers.ForEach(g => context.Groomers.Add(g));
            context.SaveChanges();

        }
    }
}
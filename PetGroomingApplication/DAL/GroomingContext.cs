using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;


namespace PetGroomingApplication.DAL
{
    public class GroomingContext : DbContext
    {

        public GroomingContext() : base("GroomingContext")
        {
        }

        public DbSet<GroomerModel> Groomers { get; set; }
        //public DbSet<Enrollment> Enrollments { get; set; }
        //public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    
    
    }
}
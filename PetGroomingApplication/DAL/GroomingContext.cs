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

        public GroomingContext() 
            : base("GroomingContext") // implicitly the connection string name = class name
        {
        }

        public DbSet<Groomer> Groomers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }


    }
}
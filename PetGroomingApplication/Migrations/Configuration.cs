namespace PetGroomingApplication.Migrations
{
    using PetGroomingApplication.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PetGroomingApplication.DAL.GroomingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(PetGroomingApplication.DAL.GroomingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            // var groomers = new List<Groomer>
            // {
            //     new Groomer {  GroomerID = new Guid("92c665ad-a54d-ea11-bf15-083e8eba6e02"), Name = "Paul", StartWorkTime = DateTime.Parse("2005-09-01 08:00"), EndWorkTime = DateTime.Parse("2005-09-01 16:00"), Specializing = Species.Dog },
            //     new Groomer {  GroomerID = new Guid("93c665ad-a54d-ea11-bf15-083e8eba6e02"), Name = "Andra", StartWorkTime = DateTime.Parse("2005-09-01 09:00"), EndWorkTime = DateTime.Parse("2005-09-01 17:00"), Specializing = Species.Dog },
            //     new Groomer {  GroomerID = new Guid("94c665ad-a54d-ea11-bf15-083e8eba6e02"), Name = "Alina", StartWorkTime = DateTime.Parse("2005-09-01 14:00"), EndWorkTime = DateTime.Parse("2005-09-01 22:00"), Specializing = Species.Dog }
            // };
            // groomers.ForEach(g => context.Groomers.AddOrUpdate(g));
            // context.SaveChanges();

            // var services = new List<Service>
            // {
            //     new Service {  ServiceID = new Guid("95c665ad-a54d-ea11-bf15-083e8eba6e02"),Species = Species.Dog, Name  ="Full Service Dog Grooming" , DurationInMinutes=180, Rate= 75},
            //     new Service {  ServiceID = new Guid("96c665ad-a54d-ea11-bf15-083e8eba6e02"),Species = Species.Dog, Name  ="Regular Service Dog Grooming" , DurationInMinutes=120, Rate= 40},
            //     new Service {  ServiceID = new Guid("97c665ad-a54d-ea11-bf15-083e8eba6e02"),Species = Species.Dog, Name  ="De-matting Dog" , DurationInMinutes=60, Rate= 30},
            //     new Service {  ServiceID = new Guid("98c665ad-a54d-ea11-bf15-083e8eba6e02"),Species = Species.Cat, Name  ="De-matting Cat" , DurationInMinutes=30, Rate= 25},
            //     new Service {  ServiceID = new Guid("99c665ad-a54d-ea11-bf15-083e8eba6e02"),Species = Species.Cat, Name  ="Lion Cut Cat" , DurationInMinutes=60, Rate= 50},
            //     new Service {  ServiceID = new Guid("9ac665ad-a54d-ea11-bf15-083e8eba6e02"),Species = Species.Cat, Name  ="Coat Dry Cleaning" , DurationInMinutes=60, Rate= 40},
            //};
            // services.ForEach(s => context.Services.AddOrUpdate(s));
            // context.SaveChanges();

             var pets = new List<Pet>
             {
                 new Pet {  PetID = new Guid("9bc665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Rex", Species = Species.Dog, Breed="German Shepherd", Age=2, OwnerID = new Guid("d5d67940-0254-ea11-bf16-083e8eba6e02")},
                 new Pet {  PetID = new Guid("9cc665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Napoleon", Species = Species.Dog, Breed="Chiwawa", Age=3, OwnerID = new Guid("d5d67940-0254-ea11-bf16-083e8eba6e02") },
                 new Pet {  PetID = new Guid("9dc665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Spot", Species = Species.Dog, Breed="Dalmatian", Age=5, OwnerID = new Guid("d5d67940-0254-ea11-bf16-083e8eba6e02") },
                 new Pet {  PetID = new Guid("9ec665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Romulus", Species = Species.Dog, Breed="Labrador", Age=1, OwnerID = new Guid("d5d67940-0254-ea11-bf16-083e8eba6e02") },
            //     new Pet {  PetID = new Guid("9fc665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Bella", Species = Species.Dog, Breed="Border Collie", Age=6 , OwnerID = new Guid("")},
            //     new Pet {  PetID = new Guid("a0c665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Booby", Species = Species.Dog, Breed="Mops", Age=4, OwnerID = "" },
            //     new Pet {  PetID = new Guid("a1c665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Terry", Species = Species.Dog, Breed="Yorkshire Terrier", Age=5, OwnerID = "" },
            //     new Pet {  PetID = new Guid("a2c665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Lara", Species = Species.Dog, Breed="German Shepherd", Age=10, OwnerID = "" },
            //     new Pet {  PetID = new Guid("a3c665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Tom", Species = Species.Cat, Breed="British Shorthair", Age=5, OwnerID = "" },
            //     new Pet {  PetID = new Guid("a4c665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Leo", Species = Species.Cat, Breed="Birman", Age=2 , OwnerID = ""},
            //     new Pet {  PetID = new Guid("a5c665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Katty", Species = Species.Cat, Breed="Domestic Shorthair", Age=6 , OwnerID = ""},
            //     new Pet {  PetID = new Guid("a6c665ad-a54d-ea11-bf15-083e8eba6e02"),Name  ="Bruno", Species = Species.Cat, Breed="Persian", Age=4, OwnerID = "" }
             };
             pets.ForEach(p => context.Pets.AddOrUpdate(p));
             context.SaveChanges();


            var appointments = new List<Appointment>
            {
            new Appointment {AppointmentID = new Guid(), ServiceID= new Guid("95c665ad-a54d-ea11-bf15-083e8eba6e02"), DateTime=new DateTime(2020,02,13, 10,00,0),GroomerID=new Guid("92c665ad-a54d-ea11-bf15-083e8eba6e02"), PetID=new Guid("9cc665ad-a54d-ea11-bf15-083e8eba6e02")},
            new Appointment {AppointmentID = new Guid(), ServiceID= new Guid("96c665ad-a54d-ea11-bf15-083e8eba6e02"), DateTime=new DateTime(2020,02,13, 13,00,0),GroomerID=new Guid("92c665ad-a54d-ea11-bf15-083e8eba6e02"), PetID=new Guid("9ec665ad-a54d-ea11-bf15-083e8eba6e02")},
            new Appointment {AppointmentID = new Guid(), ServiceID= new Guid("95c665ad-a54d-ea11-bf15-083e8eba6e02"), DateTime=new DateTime(2020,02,14, 10,00,0),GroomerID=new Guid("92c665ad-a54d-ea11-bf15-083e8eba6e02"), PetID=new Guid("9cc665ad-a54d-ea11-bf15-083e8eba6e02")},
            new Appointment {AppointmentID = new Guid(), ServiceID= new Guid("95c665ad-a54d-ea11-bf15-083e8eba6e02"), DateTime=new DateTime(2020,02,13, 10,00,0),GroomerID=new Guid("93c665ad-a54d-ea11-bf15-083e8eba6e02"), PetID=new Guid("9cc665ad-a54d-ea11-bf15-083e8eba6e02")},
            new Appointment {AppointmentID = new Guid(), ServiceID= new Guid("97c665ad-a54d-ea11-bf15-083e8eba6e02"), DateTime=new DateTime(2020,02,13, 08,00,0),GroomerID=new Guid("92c665ad-a54d-ea11-bf15-083e8eba6e02"), PetID=new Guid("9cc665ad-a54d-ea11-bf15-083e8eba6e02")},
            };
            appointments.ForEach(a => context.Appointments.AddOrUpdate(a));
            context.SaveChanges();
        }
    }
}

using PetGroomingApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PetGroomingApplication.DAL
{
    public class GroomingInitializer :
        System.Data.Entity.DropCreateDatabaseIfModelChanges<GroomingContext>
    {
        protected override void Seed(GroomingContext context)
        {
            var groomers = new List<Groomer>
            {
                new Groomer {  GroomerID = new Guid(), Name = "Paul", StartWorkTime = DateTime.Parse("2005-09-01 08:00"), EndWorkTime = DateTime.Parse("2005-09-01 16:00"), Specializing = Species.Dog },
                new Groomer {  GroomerID = new Guid(), Name = "Andra", StartWorkTime = DateTime.Parse("2005-09-01 09:00"), EndWorkTime = DateTime.Parse("2005-09-01 17:00"), Specializing = Species.Dog },
                new Groomer {  GroomerID = new Guid(), Name = "Alina", StartWorkTime = DateTime.Parse("2005-09-01 14:00"), EndWorkTime = DateTime.Parse("2005-09-01 22:00"), Specializing = Species.Dog }
            };
            groomers.ForEach(g => context.Groomers.Add(g));
            context.SaveChanges();

            var services = new List<Service>
            {
                new Service {  ServiceID = new Guid(),Species = Species.Dog, Name  ="Full Service Dog Grooming" , DurationInMinutes=180, Rate= 75},
                new Service {  ServiceID = new Guid(),Species = Species.Dog, Name  ="Regular Service Dog Grooming" , DurationInMinutes=120, Rate= 40},
                new Service {  ServiceID = new Guid(),Species = Species.Dog, Name  ="De-matting Dog" , DurationInMinutes=60, Rate= 30},
                new Service {  ServiceID = new Guid(),Species = Species.Cat, Name  ="De-matting Cat" , DurationInMinutes=30, Rate= 25},
                new Service {  ServiceID = new Guid(),Species = Species.Cat, Name  ="Lion Cut Cat" , DurationInMinutes=60, Rate= 50},
                new Service {  ServiceID = new Guid(),Species = Species.Cat, Name  ="Coat Dry Cleaning" , DurationInMinutes=60, Rate= 40},
           };
            services.ForEach(s => context.Services.Add(s));
            context.SaveChanges();

            var pets = new List<Pet>
            {
                new Pet {  PetID = new Guid(),Name  ="Rex", Species = Species.Dog, Breed="German Shepherd", Age=2 },
                new Pet {  PetID = new Guid(),Name  ="Napoleon", Species = Species.Dog, Breed="Chiwawa", Age=3 },
                new Pet {  PetID = new Guid(),Name  ="Spot", Species = Species.Dog, Breed="Dalmatian", Age=5 },
                new Pet {  PetID = new Guid(),Name  ="Romulus", Species = Species.Dog, Breed="Labrador", Age=1 },
                new Pet {  PetID = new Guid(),Name  ="Bella", Species = Species.Dog, Breed="Border Collie", Age=6 },
                new Pet {  PetID = new Guid(),Name  ="Booby", Species = Species.Dog, Breed="Mops", Age=4 },
                new Pet {  PetID = new Guid(),Name  ="Terry", Species = Species.Dog, Breed="Yorkshire Terrier", Age=5 },
                new Pet {  PetID = new Guid(),Name  ="Lara", Species = Species.Dog, Breed="German Shepherd", Age=10 },
                new Pet {  PetID = new Guid(),Name  ="Tom", Species = Species.Cat, Breed="British Shorthair", Age=5 },
                new Pet {  PetID = new Guid(),Name  ="Leo", Species = Species.Cat, Breed="Birman", Age=2 },
                new Pet {  PetID = new Guid(),Name  ="Katty", Species = Species.Cat, Breed="Domestic Shorthair", Age=6 },
                new Pet {  PetID = new Guid(),Name  ="Bruno", Species = Species.Cat, Breed="Persian", Age=4 }
            };
            pets.ForEach(p => context.Pets.Add(p));
            context.SaveChanges();
        }
    }
}
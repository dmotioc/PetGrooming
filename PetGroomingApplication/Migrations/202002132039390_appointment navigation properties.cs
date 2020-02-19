namespace PetGroomingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentnavigationproperties : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Appointments", "GroomerID");
            CreateIndex("dbo.Appointments", "PetID");
            AddForeignKey("dbo.Appointments", "GroomerID", "dbo.Groomers", "GroomerID", cascadeDelete: true);
            AddForeignKey("dbo.Appointments", "PetID", "dbo.Pets", "PetID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "PetID", "dbo.Pets");
            DropForeignKey("dbo.Appointments", "GroomerID", "dbo.Groomers");
            DropIndex("dbo.Appointments", new[] { "PetID" });
            DropIndex("dbo.Appointments", new[] { "GroomerID" });
        }
    }
}

namespace PetGroomingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentID = c.Guid(nullable: false, identity: true),
                        ServiceID = c.Guid(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        DurationInMinutes = c.Int(nullable: false),
                        GroomerID = c.Guid(nullable: false),
                        PetID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Appointments");
        }
    }
}

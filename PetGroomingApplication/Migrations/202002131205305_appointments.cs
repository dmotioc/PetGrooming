namespace PetGroomingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointments : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Appointments", "ServiceID");
            AddForeignKey("dbo.Appointments", "ServiceID", "dbo.Services", "ServiceID", cascadeDelete: true);
            DropColumn("dbo.Appointments", "DurationInMinutes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "DurationInMinutes", c => c.Int(nullable: false));
            DropForeignKey("dbo.Appointments", "ServiceID", "dbo.Services");
            DropIndex("dbo.Appointments", new[] { "ServiceID" });
        }
    }
}

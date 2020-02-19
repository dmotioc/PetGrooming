namespace PetGroomingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOwnerModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        OwnerID = c.Guid(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OwnerID);
            
            AddColumn("dbo.Groomers", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Pets", "Owner_OwnerID", c => c.Guid());
            CreateIndex("dbo.Pets", "Owner_OwnerID");
            AddForeignKey("dbo.Pets", "Owner_OwnerID", "dbo.Owners", "OwnerID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pets", "Owner_OwnerID", "dbo.Owners");
            DropIndex("dbo.Pets", new[] { "Owner_OwnerID" });
            DropColumn("dbo.Pets", "Owner_OwnerID");
            DropColumn("dbo.Groomers", "UserId");
            DropTable("dbo.Owners");
        }
    }
}

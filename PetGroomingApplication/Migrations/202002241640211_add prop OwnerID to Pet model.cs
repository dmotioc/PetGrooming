namespace PetGroomingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpropOwnerIDtoPetmodel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Pets", name: "Owner_OwnerID", newName: "OwnerID");
            AlterColumn("dbo.Pets", "OwnerID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Pets", "OwnerID");
            AddForeignKey("dbo.Pets", "OwnerID", "dbo.Owners", "OwnerID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pets", "OwnerID", "dbo.Owners");
            DropIndex("dbo.Pets", new[] { "OwnerID" });
            AlterColumn("dbo.Pets", "OwnerID", c => c.Guid());
            RenameColumn(table: "dbo.Pets", name: "OwnerID", newName: "Owner_OwnerID");
            CreateIndex("dbo.Pets", "Owner_OwnerID");
            AddForeignKey("dbo.Pets", "Owner_OwnerID", "dbo.Owners", "OwnerID");
        }
    }
}

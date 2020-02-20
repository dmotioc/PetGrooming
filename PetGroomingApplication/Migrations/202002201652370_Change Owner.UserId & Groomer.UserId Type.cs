namespace PetGroomingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOwnerUserIdGroomerUserIdType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Groomers", "UserId", c => c.String());
            AlterColumn("dbo.Owners", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Owners", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Groomers", "UserId", c => c.Int(nullable: false));
        }
    }
}

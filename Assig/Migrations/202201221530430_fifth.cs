namespace Assig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Machines", "UserID", "dbo.Users");
            DropIndex("dbo.Machines", new[] { "UserID" });
            AddColumn("dbo.Machines", "MachineName", c => c.String());
            AddColumn("dbo.Machines", "PhoneNumber", c => c.String());
            DropColumn("dbo.Machines", "Approved");
            DropColumn("dbo.Machines", "Name");
            DropColumn("dbo.Machines", "UserID");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        Email = c.String(),
                        Blocked = c.Boolean(nullable: false),
                        CurrentRentedMaterialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.Machines", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Machines", "Name", c => c.String());
            AddColumn("dbo.Machines", "Approved", c => c.Boolean(nullable: false));
            DropColumn("dbo.Machines", "PhoneNumber");
            DropColumn("dbo.Machines", "MachineName");
            CreateIndex("dbo.Machines", "UserID");
            AddForeignKey("dbo.Machines", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
    }
}

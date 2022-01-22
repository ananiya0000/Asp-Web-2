namespace Assig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fivth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Machine_MachineId", "dbo.Machines");
            DropIndex("dbo.Users", new[] { "Machine_MachineId" });
            DropPrimaryKey("dbo.Users");
            AddColumn("dbo.Machines", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UserID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Users", "userName", c => c.String());
            AddPrimaryKey("dbo.Users", "UserID");
            CreateIndex("dbo.Machines", "UserID");
            AddForeignKey("dbo.Machines", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            DropColumn("dbo.Users", "Machine_MachineId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Machine_MachineId", c => c.Int());
            DropForeignKey("dbo.Machines", "UserID", "dbo.Users");
            DropIndex("dbo.Machines", new[] { "UserID" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "userName", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Users", "UserID");
            DropColumn("dbo.Machines", "UserID");
            AddPrimaryKey("dbo.Users", "userName");
            CreateIndex("dbo.Users", "Machine_MachineId");
            AddForeignKey("dbo.Users", "Machine_MachineId", "dbo.Machines", "MachineId");
        }
    }
}

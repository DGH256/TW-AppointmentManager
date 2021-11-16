namespace AppointmentManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPersonalSettings", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserPersonalSettings", new[] { "Id" });
            DropPrimaryKey("dbo.UserPersonalSettings");
            AddColumn("dbo.AspNetUsers", "UserPersonalSettings_Id", c => c.Int());
            AlterColumn("dbo.UserPersonalSettings", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserPersonalSettings", "Id");
            CreateIndex("dbo.AspNetUsers", "UserPersonalSettings_Id");
            AddForeignKey("dbo.AspNetUsers", "UserPersonalSettings_Id", "dbo.UserPersonalSettings", "Id");
        }
        
        public override void Down()
        {           
            DropForeignKey("dbo.AspNetUsers", "UserPersonalSettings_Id", "dbo.UserPersonalSettings");
            DropIndex("dbo.AspNetUsers", new[] { "UserPersonalSettings_Id" });
            DropPrimaryKey("dbo.UserPersonalSettings");
            AlterColumn("dbo.UserPersonalSettings", "Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUsers", "UserPersonalSettings_Id");
            AddPrimaryKey("dbo.UserPersonalSettings", "Id");
            CreateIndex("dbo.UserPersonalSettings", "Id");
            AddForeignKey("dbo.UserPersonalSettings", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}

namespace AppointmentManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSettings_changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserPersonalSettings_Id", "dbo.UserPersonalSettings");
            DropIndex("dbo.AspNetUsers", new[] { "UserPersonalSettings_Id" });
            AddColumn("dbo.AspNetUsers", "Nickname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "Details", c => c.String());
            AddColumn("dbo.AspNetUsers", "CNP", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserPersonalSettings_Id");
            DropTable("dbo.UserPersonalSettings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPersonalSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_User = c.String(),
                        Name = c.String(),
                        Phone_number = c.String(),
                        Email_address = c.String(),
                        New_password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "UserPersonalSettings_Id", c => c.Int());
            DropColumn("dbo.AspNetUsers", "CNP");
            DropColumn("dbo.AspNetUsers", "Details");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Nickname");
            CreateIndex("dbo.AspNetUsers", "UserPersonalSettings_Id");
            AddForeignKey("dbo.AspNetUsers", "UserPersonalSettings_Id", "dbo.UserPersonalSettings", "Id");
        }
    }
}

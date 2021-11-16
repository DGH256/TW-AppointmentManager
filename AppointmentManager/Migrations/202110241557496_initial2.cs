namespace AppointmentManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPersonalSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_User = c.String(maxLength: 128),
                        Name = c.String(),
                        Phone_number = c.String(),
                        Email_address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id_User)
                .Index(t => t.Id_User);
            

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPersonalSettings", "Id_User", "dbo.AspNetUsers");
            DropIndex("dbo.UserPersonalSettings", new[] { "Id_User" });
            DropTable("dbo.UserPersonalSettings");
        }
    }
}

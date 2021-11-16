namespace AppointmentManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appointments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_User = c.String(nullable: false, maxLength: 128),
                        Id_Customer = c.Int(nullable: false),
                        Start_datetime = c.DateTime(nullable: false),
                        End_datetime = c.DateTime(nullable: false),
                        Details = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id_Customer, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Id_User, cascadeDelete: true)
                .Index(t => t.Id_User)
                .Index(t => t.Id_Customer);
            
            AddColumn("dbo.AspNetUsers", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "Id_User", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "Id_Customer", "dbo.Customers");
            DropIndex("dbo.Appointments", new[] { "Id_Customer" });
            DropIndex("dbo.Appointments", new[] { "Id_User" });
            DropColumn("dbo.AspNetUsers", "isDeleted");
            DropTable("dbo.Appointments");
        }
    }
}

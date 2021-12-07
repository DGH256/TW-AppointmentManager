namespace AppointmentManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booking_db_relations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Booking_Event", "description", c => c.String());
            AddColumn("dbo.Booking_Event", "customerId", c => c.Int(nullable: true));
            AlterColumn("dbo.Booking_Event", "resourceId", c => c.Int(nullable: true));
            CreateIndex("dbo.Booking_Event", "customerId");
            CreateIndex("dbo.Booking_Event", "resourceId");
            AddForeignKey("dbo.Booking_Event", "resourceId", "dbo.Booking_Room", "id", cascadeDelete: true);
            AddForeignKey("dbo.Booking_Event", "customerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booking_Event", "customerId", "dbo.Customers");
            DropForeignKey("dbo.Booking_Event", "resourceId", "dbo.Booking_Room");
            DropIndex("dbo.Booking_Event", new[] { "resourceId" });
            DropIndex("dbo.Booking_Event", new[] { "customerId" });
            AlterColumn("dbo.Booking_Event", "resourceId", c => c.String());
            DropColumn("dbo.Booking_Event", "customerId");
            DropColumn("dbo.Booking_Event", "description");
        }
    }
}

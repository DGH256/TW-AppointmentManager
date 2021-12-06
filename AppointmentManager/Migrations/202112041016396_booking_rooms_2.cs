namespace AppointmentManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booking_rooms_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Booking_Room", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Booking_Room", "isDeleted");
        }
    }
}

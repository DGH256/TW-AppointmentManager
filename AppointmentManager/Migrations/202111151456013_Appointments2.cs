namespace AppointmentManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appointments2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarAppointments", "Id_User", c => c.String());
            AddColumn("dbo.CalendarAppointments", "Id_Customer", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalendarAppointments", "Id_Customer");
            DropColumn("dbo.CalendarAppointments", "Id_User");
        }
    }
}

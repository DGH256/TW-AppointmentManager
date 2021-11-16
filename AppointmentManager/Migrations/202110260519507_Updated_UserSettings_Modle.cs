namespace AppointmentManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated_UserSettings_Modle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPersonalSettings", "New_password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPersonalSettings", "New_password");
        }
    }
}

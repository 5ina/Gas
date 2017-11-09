namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Vehicle_Scripts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "CarPhead", c => c.String(nullable: false, maxLength: 4));
            AlterColumn("dbo.Vehicles", "CartNumber", c => c.String(nullable: false, maxLength: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "CartNumber", c => c.String(nullable: false, maxLength: 4));
            AlterColumn("dbo.Vehicles", "CarPhead", c => c.String());
        }
    }
}

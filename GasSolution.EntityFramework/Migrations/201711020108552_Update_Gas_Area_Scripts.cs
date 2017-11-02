namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Gas_Area_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GasStations", "AreaId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GasStations", "AreaId");
        }
    }
}

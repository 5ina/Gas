namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Gas_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GasStations", "Address", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GasStations", "Address");
        }
    }
}

namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Gas_Contacts_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GasStations", "Contacts", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.GasStations", "Tel", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GasStations", "Tel");
            DropColumn("dbo.GasStations", "Contacts");
        }
    }
}

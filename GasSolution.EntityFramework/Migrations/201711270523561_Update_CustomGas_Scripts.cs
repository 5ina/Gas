namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_CustomGas_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GasStationCustoms", "Audit", c => c.Int(nullable: false));
            AddColumn("dbo.GasStationCustoms", "AuditReason", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GasStationCustoms", "AuditReason");
            DropColumn("dbo.GasStationCustoms", "Audit");
        }
    }
}

namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Gas_Promotion_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GasStations", "FixedPromotion", c => c.Boolean(nullable: false));
            AddColumn("dbo.GasStations", "StartTime", c => c.DateTime());
            AddColumn("dbo.GasStations", "EndTime", c => c.DateTime());
            AddColumn("dbo.GasStations", "PromotionNotice", c => c.String(maxLength: 1000));
            AddColumn("dbo.GasStations", "Gasoline_Eighty_Nine", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.GasStations", "Gasoline_Ninety_Two", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.GasStations", "Gasoline_Ninety_Fine", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.GasStations", "Gasoline_Ninety_Eight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.GasStations", "Natural", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GasStations", "Natural");
            DropColumn("dbo.GasStations", "Gasoline_Ninety_Eight");
            DropColumn("dbo.GasStations", "Gasoline_Ninety_Fine");
            DropColumn("dbo.GasStations", "Gasoline_Ninety_Two");
            DropColumn("dbo.GasStations", "Gasoline_Eighty_Nine");
            DropColumn("dbo.GasStations", "PromotionNotice");
            DropColumn("dbo.GasStations", "EndTime");
            DropColumn("dbo.GasStations", "StartTime");
            DropColumn("dbo.GasStations", "FixedPromotion");
        }
    }
}

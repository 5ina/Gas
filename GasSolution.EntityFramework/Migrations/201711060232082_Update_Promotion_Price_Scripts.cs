namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Promotion_Price_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotions", "Gasoline_Eighty_Nine", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Gasoline_Ninety_Two", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Gasoline_Ninety_Fine", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Gasoline_Ninety_Eight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Natural", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Promotions", "Gasoline_Market_Eighty_Nine");
            DropColumn("dbo.Promotions", "Gasoline_Price_Eighty_Nine");
            DropColumn("dbo.Promotions", "Gasoline_Market_Ninety_Two");
            DropColumn("dbo.Promotions", "Gasoline_Price_Ninety_Two");
            DropColumn("dbo.Promotions", "Gasoline_Market_Ninety_Fine");
            DropColumn("dbo.Promotions", "Gasoline_Price_Ninety_Fine");
            DropColumn("dbo.Promotions", "Gasoline_Market_Ninety_Eight");
            DropColumn("dbo.Promotions", "Gasoline_Price_Ninety_Eight");
            DropColumn("dbo.Promotions", "Natural_Market");
            DropColumn("dbo.Promotions", "Natural_Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Promotions", "Natural_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Natural_Market", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Gasoline_Price_Ninety_Eight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Gasoline_Market_Ninety_Eight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Gasoline_Price_Ninety_Fine", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Gasoline_Market_Ninety_Fine", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Gasoline_Price_Ninety_Two", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Gasoline_Market_Ninety_Two", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Gasoline_Price_Eighty_Nine", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Promotions", "Gasoline_Market_Eighty_Nine", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Promotions", "Natural");
            DropColumn("dbo.Promotions", "Gasoline_Ninety_Eight");
            DropColumn("dbo.Promotions", "Gasoline_Ninety_Fine");
            DropColumn("dbo.Promotions", "Gasoline_Ninety_Two");
            DropColumn("dbo.Promotions", "Gasoline_Eighty_Nine");
        }
    }
}

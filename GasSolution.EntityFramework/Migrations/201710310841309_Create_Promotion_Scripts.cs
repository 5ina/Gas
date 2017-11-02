namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Promotion_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GasStationId = c.Int(nullable: false),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        Notice = c.String(maxLength: 1000),
                        Gasoline_Market_Eighty_Nine = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gasoline_Price_Eighty_Nine = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gasoline_Market_Ninety_Two = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gasoline_Price_Ninety_Two = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gasoline_Market_Ninety_Fine = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gasoline_Price_Ninety_Fine = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gasoline_Market_Ninety_Eight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gasoline_Price_Ninety_Eight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Natural_Market = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Natural_Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Promotions");
        }
    }
}

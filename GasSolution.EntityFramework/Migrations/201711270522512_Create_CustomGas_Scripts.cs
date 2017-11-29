namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_CustomGas_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GasStationCustoms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Longitude = c.String(nullable: false, maxLength: 120),
                        Dimension = c.String(nullable: false, maxLength: 120),
                        Address = c.String(maxLength: 500),
                        IsClose = c.Boolean(nullable: false),
                        Notice = c.String(maxLength: 1000),
                        Contacts = c.String(maxLength: 30),
                        Tel = c.String(maxLength: 30),
                        AreaId = c.Int(nullable: false),
                        FixedPromotion = c.Boolean(nullable: false),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        PromotionNotice = c.String(maxLength: 1000),
                        Gasoline_Eighty_Nine = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gasoline_Ninety_Two = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gasoline_Ninety_Fine = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gasoline_Ninety_Eight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Natural = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationTime = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GasStationCustoms");
        }
    }
}

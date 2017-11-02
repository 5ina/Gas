namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Gas_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mobile = c.String(maxLength: 15),
                        NickName = c.String(maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 60),
                        OpenId = c.String(maxLength: 60),
                        CustomerRoleId = c.Int(nullable: false),
                        PasswordSalt = c.String(nullable: false, maxLength: 6),
                        LastModificationTime = c.DateTime(),
                        CreationTime = c.DateTime(nullable: false),
                        VerificationCode = c.String(maxLength: 200),
                        IsSubscribe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Key = c.String(nullable: false, maxLength: 200),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GasStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GasName = c.String(nullable: false, maxLength: 120),
                        IsGasoLine = c.Boolean(nullable: false),
                        GasoLineLength = c.Int(nullable: false),
                        IsDieselOil = c.Boolean(nullable: false),
                        DieselOilLength = c.Int(nullable: false),
                        IsNatural = c.Boolean(nullable: false),
                        NaturalLength = c.Int(nullable: false),
                        Longitude = c.String(nullable: false, maxLength: 120),
                        Dimension = c.String(nullable: false, maxLength: 120),
                        IsClose = c.Boolean(nullable: false),
                        Notice = c.String(maxLength: 1000),
                        DisplayOrder = c.Int(nullable: false),
                        LastModificationTime = c.DateTime(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
            DropTable("dbo.GasStations");
            DropTable("dbo.CustomerAttributes");
            DropTable("dbo.Customers");
        }
    }
}

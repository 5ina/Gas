namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Vehicle_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CarPhead = c.String(),
                        CartNumber = c.String(nullable: false, maxLength: 4),
                        FrameNo = c.String(nullable: false, maxLength: 6),
                        EngineNo = c.String(nullable: false, maxLength: 6),
                        Color = c.Int(nullable: false),
                        LastModificationTime = c.DateTime(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vehicles");
        }
    }
}

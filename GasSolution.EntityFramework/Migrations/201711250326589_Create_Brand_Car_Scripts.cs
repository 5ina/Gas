namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Brand_Car_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarBrands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Initial = c.String(nullable: false, maxLength: 2),
                        Logo = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarBrands");
        }
    }
}

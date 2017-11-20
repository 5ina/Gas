namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_KeyFont_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KeyFonts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Keyword = c.String(nullable: false, maxLength: 40),
                        Relpys = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KeyFonts");
        }
    }
}

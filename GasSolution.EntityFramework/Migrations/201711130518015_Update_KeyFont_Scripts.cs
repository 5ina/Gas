namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_KeyFont_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KeyFonts", "KeyFontType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KeyFonts", "KeyFontType");
        }
    }
}

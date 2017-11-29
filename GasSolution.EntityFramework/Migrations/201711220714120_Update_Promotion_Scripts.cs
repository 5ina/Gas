namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Promotion_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotions", "CreatorUserId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Promotions", "CreatorUserId");
        }
    }
}

namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Promotion_Use_Customer_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotions", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Promotions", "CreatorUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Promotions", "CreatorUserId", c => c.Long());
            DropColumn("dbo.Promotions", "CustomerId");
        }
    }
}

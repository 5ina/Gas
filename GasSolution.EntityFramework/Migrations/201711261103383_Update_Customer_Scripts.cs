namespace GasSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Customer_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CustomerGuid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "CustomerGuid");
        }
    }
}

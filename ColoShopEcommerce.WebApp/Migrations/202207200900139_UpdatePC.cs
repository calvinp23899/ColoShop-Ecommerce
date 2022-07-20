namespace ColoShopEcommerce.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePC : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductCategory", "Alias", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductCategory", "Alias", c => c.String(nullable: false, maxLength: 150));
        }
    }
}

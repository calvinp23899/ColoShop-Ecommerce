namespace ColoShopEcommerce.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "PriceSale", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Product", "IsHome");
            DropColumn("dbo.Product", "IsFeature");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "IsFeature", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "IsHome", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Product", "PriceSale", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

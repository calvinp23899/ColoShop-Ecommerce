namespace ColoShopEcommerce.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Order", "IsDelivery", c => c.Boolean(nullable: false));
            AddColumn("dbo.Order", "IsPaid", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Order", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "Code", c => c.String(nullable: false));
            DropColumn("dbo.Order", "IsPaid");
            DropColumn("dbo.Order", "IsDelivery");
            DropColumn("dbo.Order", "Email");
        }
    }
}

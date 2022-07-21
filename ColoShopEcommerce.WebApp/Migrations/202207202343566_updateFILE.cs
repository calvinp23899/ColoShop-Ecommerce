namespace ColoShopEcommerce.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFILE : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FILE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Path = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            DropTable("dbo.ProductImage");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Image = c.String(),
                        IsDefault = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.FILE", "ProductId", "dbo.Product");
            DropIndex("dbo.FILE", new[] { "ProductId" });
            DropTable("dbo.FILE");
        }
    }
}

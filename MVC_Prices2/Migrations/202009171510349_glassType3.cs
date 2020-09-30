namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class glassType3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Glasses", "Product_Id", "dbo.Products");
            DropIndex("dbo.Glasses", new[] { "Product_Id" });
            DropColumn("dbo.Glasses", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Glasses", "Product_Id", c => c.Int());
            CreateIndex("dbo.Glasses", "Product_Id");
            AddForeignKey("dbo.Glasses", "Product_Id", "dbo.Products", "Id");
        }
    }
}

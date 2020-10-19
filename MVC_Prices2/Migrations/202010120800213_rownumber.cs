namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rownumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "RowNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "RowNumber");
        }
    }
}

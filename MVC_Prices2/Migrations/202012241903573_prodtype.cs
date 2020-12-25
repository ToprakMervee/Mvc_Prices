namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prodtype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PType", c => c.String(maxLength: 70));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PType");
        }
    }
}

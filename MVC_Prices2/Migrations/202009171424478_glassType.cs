namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class glassType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Glasses", "GlassType", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Glasses", "GlassType");
        }
    }
}

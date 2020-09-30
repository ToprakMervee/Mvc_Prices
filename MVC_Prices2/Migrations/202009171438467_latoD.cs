namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latoD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfferDets", "LatoD", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OfferDets", "LatoD");
        }
    }
}

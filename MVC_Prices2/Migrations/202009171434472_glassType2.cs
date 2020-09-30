namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class glassType2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfferDets", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OfferDets", "Note");
        }
    }
}

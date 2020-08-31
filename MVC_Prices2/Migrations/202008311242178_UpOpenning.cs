namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpOpenning : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfferDets", "UpOpenning", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OfferDets", "UpOpenning");
        }
    }
}

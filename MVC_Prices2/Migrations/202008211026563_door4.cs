namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class door4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfferDets", "DoorHandle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OfferDets", "DoorHandle");
        }
    }
}

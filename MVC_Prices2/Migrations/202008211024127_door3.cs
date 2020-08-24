namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class door3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OfferDets", "DoorHandle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OfferDets", "DoorHandle", c => c.String());
        }
    }
}

namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class offerDetExtra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfferDets", "Extra", c => c.String(maxLength: 350));
            AlterColumn("dbo.OfferDets", "DoorHandle", c => c.String(maxLength: 150));
            AlterColumn("dbo.OfferDets", "LatchArm", c => c.String(maxLength: 150));
            AlterColumn("dbo.OfferDets", "UpOpenning", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OfferDets", "UpOpenning", c => c.String());
            AlterColumn("dbo.OfferDets", "LatchArm", c => c.String());
            AlterColumn("dbo.OfferDets", "DoorHandle", c => c.String());
            DropColumn("dbo.OfferDets", "Extra");
        }
    }
}

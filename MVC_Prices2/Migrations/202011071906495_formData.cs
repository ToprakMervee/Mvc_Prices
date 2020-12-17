namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class formData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfferDets", "FormData", c => c.String(maxLength: 550));
            AddColumn("dbo.OfferDets", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.OfferDets", "Extra", c => c.String(maxLength: 1200));
            AlterColumn("dbo.OfferDets", "Note", c => c.String(maxLength: 150));
            DropColumn("dbo.OfferDets", "ArmType");
            DropColumn("dbo.OfferDets", "LatchArm");
            DropColumn("dbo.OfferDets", "UpOpenning");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OfferDets", "UpOpenning", c => c.String(maxLength: 500));
            AddColumn("dbo.OfferDets", "LatchArm", c => c.String(maxLength: 150));
            AddColumn("dbo.OfferDets", "ArmType", c => c.String(maxLength: 50));
            AlterColumn("dbo.OfferDets", "Note", c => c.String());
            AlterColumn("dbo.OfferDets", "Extra", c => c.String(maxLength: 1000));
            DropColumn("dbo.OfferDets", "Description");
            DropColumn("dbo.OfferDets", "FormData");
        }
    }
}

namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extraleng : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OfferDets", "UpOpenning", c => c.String(maxLength: 500));
            AlterColumn("dbo.OfferDets", "Extra", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OfferDets", "Extra", c => c.String(maxLength: 350));
            AlterColumn("dbo.OfferDets", "UpOpenning", c => c.String(maxLength: 150));
        }
    }
}

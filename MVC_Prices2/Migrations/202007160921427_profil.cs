namespace MVC_Prices2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profil : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prices", "Profil", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prices", "Profil");
        }
    }
}

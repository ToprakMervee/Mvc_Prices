namespace MVC_Prices2.Migrations2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Role", c => c.String());
        }
    }
}

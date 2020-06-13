namespace MVC_Prices2.Migrations2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mstf : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "UsersRole");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UsersRole", c => c.String(maxLength: 100));
        }
    }
}

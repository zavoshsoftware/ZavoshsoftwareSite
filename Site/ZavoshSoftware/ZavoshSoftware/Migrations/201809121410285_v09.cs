namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v09 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "Order", c => c.Int(nullable: false));
            AddColumn("dbo.PageGroups", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PageGroups", "Order");
            DropColumn("dbo.Pages", "Order");
        }
    }
}

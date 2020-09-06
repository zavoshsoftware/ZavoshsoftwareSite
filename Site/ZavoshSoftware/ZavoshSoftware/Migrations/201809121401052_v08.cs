namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v08 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.PageGroups", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PageGroups", "IsActive");
            DropColumn("dbo.Pages", "IsActive");
        }
    }
}

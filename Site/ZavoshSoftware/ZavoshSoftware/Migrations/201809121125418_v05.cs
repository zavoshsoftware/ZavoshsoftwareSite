namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v05 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "Notation", c => c.String());
            AddColumn("dbo.Pages", "MetaDescription", c => c.String(maxLength: 350));
            AddColumn("dbo.PageGroups", "Notation", c => c.String());
            AddColumn("dbo.PageGroups", "MetaDescription", c => c.String(maxLength: 350));
            AlterColumn("dbo.Pages", "ImageUrl", c => c.String(maxLength: 500));
            AlterColumn("dbo.PageGroups", "ImageUrl", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PageGroups", "ImageUrl", c => c.String());
            AlterColumn("dbo.Pages", "ImageUrl", c => c.String());
            DropColumn("dbo.PageGroups", "MetaDescription");
            DropColumn("dbo.PageGroups", "Notation");
            DropColumn("dbo.Pages", "MetaDescription");
            DropColumn("dbo.Pages", "Notation");
        }
    }
}

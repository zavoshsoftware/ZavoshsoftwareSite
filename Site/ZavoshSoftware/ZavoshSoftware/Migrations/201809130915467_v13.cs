namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pages", "PageGroupId", "dbo.PageGroups");
            DropIndex("dbo.Pages", new[] { "PageGroupId" });
            AlterColumn("dbo.Pages", "PageGroupId", c => c.Guid());
            CreateIndex("dbo.Pages", "PageGroupId");
            AddForeignKey("dbo.Pages", "PageGroupId", "dbo.PageGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pages", "PageGroupId", "dbo.PageGroups");
            DropIndex("dbo.Pages", new[] { "PageGroupId" });
            AlterColumn("dbo.Pages", "PageGroupId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Pages", "PageGroupId");
            AddForeignKey("dbo.Pages", "PageGroupId", "dbo.PageGroups", "Id", cascadeDelete: true);
        }
    }
}

namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v04 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PageGroups", new[] { "ParentId" });
            AlterColumn("dbo.PageGroups", "ParentId", c => c.Guid());
            CreateIndex("dbo.PageGroups", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PageGroups", new[] { "ParentId" });
            AlterColumn("dbo.PageGroups", "ParentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PageGroups", "ParentId");
        }
    }
}

namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v02 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PageGroups", new[] { "ParentPageGroup_Id" });
            DropColumn("dbo.PageGroups", "ParentId");
            RenameColumn(table: "dbo.PageGroups", name: "ParentPageGroup_Id", newName: "ParentId");
            AlterColumn("dbo.PageGroups", "ParentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PageGroups", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PageGroups", new[] { "ParentId" });
            AlterColumn("dbo.PageGroups", "ParentId", c => c.Guid());
            RenameColumn(table: "dbo.PageGroups", name: "ParentId", newName: "ParentPageGroup_Id");
            AddColumn("dbo.PageGroups", "ParentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.PageGroups", "ParentPageGroup_Id");
        }
    }
}

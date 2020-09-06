namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v03 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "ParentComment_Id" });
            DropColumn("dbo.Comments", "ParentId");
            RenameColumn(table: "dbo.Comments", name: "ParentComment_Id", newName: "ParentId");
            AddColumn("dbo.Comments", "PageId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Comments", "ParentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Comments", "ParentId");
            CreateIndex("dbo.Comments", "PageId");
            AddForeignKey("dbo.Comments", "PageId", "dbo.Pages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PageId", "dbo.Pages");
            DropIndex("dbo.Comments", new[] { "PageId" });
            DropIndex("dbo.Comments", new[] { "ParentId" });
            AlterColumn("dbo.Comments", "ParentId", c => c.Guid());
            DropColumn("dbo.Comments", "PageId");
            RenameColumn(table: "dbo.Comments", name: "ParentId", newName: "ParentComment_Id");
            AddColumn("dbo.Comments", "ParentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Comments", "ParentComment_Id");
        }
    }
}

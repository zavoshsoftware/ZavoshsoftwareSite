namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v06 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "ParentId" });
            AddColumn("dbo.Comments", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Comments", "ParentId", c => c.Guid());
            CreateIndex("dbo.Comments", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "ParentId" });
            AlterColumn("dbo.Comments", "ParentId", c => c.Guid(nullable: false));
            DropColumn("dbo.Comments", "IsActive");
            CreateIndex("dbo.Comments", "ParentId");
        }
    }
}

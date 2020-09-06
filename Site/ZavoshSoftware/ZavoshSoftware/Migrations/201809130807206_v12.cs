namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pages", "PositionId", "dbo.Positions");
            DropIndex("dbo.Pages", new[] { "PositionId" });
            CreateTable(
                "dbo.PagePositions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PageId = c.Guid(nullable: false),
                        PositionId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .Index(t => t.PageId)
                .Index(t => t.PositionId);
            
            AlterColumn("dbo.Positions", "Title", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Pages", "PositionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pages", "PositionId", c => c.Guid());
            DropForeignKey("dbo.PagePositions", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.PagePositions", "PageId", "dbo.Pages");
            DropIndex("dbo.PagePositions", new[] { "PositionId" });
            DropIndex("dbo.PagePositions", new[] { "PageId" });
            AlterColumn("dbo.Positions", "Title", c => c.String());
            DropTable("dbo.PagePositions");
            CreateIndex("dbo.Pages", "PositionId");
            AddForeignKey("dbo.Pages", "PositionId", "dbo.Positions", "Id");
        }
    }
}

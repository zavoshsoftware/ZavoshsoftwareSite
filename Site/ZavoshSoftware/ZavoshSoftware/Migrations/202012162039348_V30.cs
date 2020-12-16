namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V30 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Email = c.String(maxLength: 200),
                        Body = c.String(nullable: false, storeType: "ntext"),
                        IsActive = c.Boolean(nullable: false),
                        PageId = c.Guid(nullable: false),
                        Response = c.String(),
                        ResponseDate = c.DateTime(),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .Index(t => t.PageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PageId", "dbo.Pages");
            DropIndex("dbo.Comments", new[] { "PageId" });
            DropTable("dbo.Comments");
        }
    }
}

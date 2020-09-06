namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faqs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        Question = c.String(),
                        Answer = c.String(),
                        PageId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .Index(t => t.PageId);
            
            AddColumn("dbo.Pages", "HasFaq", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pages", "FaqTitle", c => c.String());
            AddColumn("dbo.Pages", "Page_Id", c => c.Guid());
            CreateIndex("dbo.Pages", "Page_Id");
            AddForeignKey("dbo.Pages", "Page_Id", "dbo.Pages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Faqs", "PageId", "dbo.Pages");
            DropForeignKey("dbo.Pages", "Page_Id", "dbo.Pages");
            DropIndex("dbo.Faqs", new[] { "PageId" });
            DropIndex("dbo.Pages", new[] { "Page_Id" });
            DropColumn("dbo.Pages", "Page_Id");
            DropColumn("dbo.Pages", "FaqTitle");
            DropColumn("dbo.Pages", "HasFaq");
            DropTable("dbo.Faqs");
        }
    }
}

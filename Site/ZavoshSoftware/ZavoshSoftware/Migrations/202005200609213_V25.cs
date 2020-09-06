namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V25 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Email = c.String(maxLength: 200),
                        Body = c.String(nullable: false, storeType: "ntext"),
                        Response = c.String(nullable: false, storeType: "ntext"),
                        IsActive = c.Boolean(nullable: false),
                        BlogId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        UrlParameter = c.String(nullable: false, maxLength: 200),
                        Summery = c.String(maxLength: 500),
                        Body = c.String(nullable: false, storeType: "ntext"),
                        ImageUrl = c.String(maxLength: 500),
                        BlogGroupId = c.Guid(),
                        Notation = c.String(),
                        MetaDescription = c.String(maxLength: 350),
                        IsActive = c.Boolean(nullable: false),
                        TitleTag = c.String(maxLength: 350),
                        Order = c.Int(nullable: false),
                        HasFaq = c.Boolean(nullable: false),
                        FaqTitle = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogGroups", t => t.BlogGroupId)
                .Index(t => t.BlogGroupId);
            
            CreateTable(
                "dbo.BlogGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        UrlParameter = c.String(nullable: false, maxLength: 200),
                        Summery = c.String(maxLength: 500),
                        Body = c.String(nullable: false, storeType: "ntext"),
                        ImageUrl = c.String(maxLength: 500),
                        Notation = c.String(),
                        MetaDescription = c.String(maxLength: 350),
                        IsActive = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "BlogGroupId", "dbo.BlogGroups");
            DropForeignKey("dbo.BlogComments", "BlogId", "dbo.Blogs");
            DropIndex("dbo.Blogs", new[] { "BlogGroupId" });
            DropIndex("dbo.BlogComments", new[] { "BlogId" });
            DropTable("dbo.BlogGroups");
            DropTable("dbo.Blogs");
            DropTable("dbo.BlogComments");
        }
    }
}

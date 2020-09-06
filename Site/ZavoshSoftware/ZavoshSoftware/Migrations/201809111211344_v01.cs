namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v01 : DbMigration
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
                        Body = c.String(nullable: false, maxLength: 200),
                        ParentId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                        ParentComment_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.ParentComment_Id)
                .Index(t => t.ParentComment_Id);
            
            CreateTable(
                "dbo.PageGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        UrlParameter = c.String(nullable: false, maxLength: 200),
                        Summery = c.String(maxLength: 500),
                        Body = c.String(nullable: false, storeType: "ntext"),
                        ImageUrl = c.String(),
                        ParentId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                        ParentPageGroup_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PageGroups", t => t.ParentPageGroup_Id)
                .Index(t => t.ParentPageGroup_Id);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        UrlParameter = c.String(nullable: false, maxLength: 200),
                        Summery = c.String(maxLength: 500),
                        Body = c.String(nullable: false, storeType: "ntext"),
                        ImageUrl = c.String(),
                        PageGroupId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PageGroups", t => t.PageGroupId, cascadeDelete: true)
                .Index(t => t.PageGroupId);
            
            CreateTable(
                "dbo.RelatedPages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SourcePageId = c.Guid(nullable: false),
                        RelatePageId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Name = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Username = c.String(nullable: false, maxLength: 256),
                        Password = c.String(nullable: false, maxLength: 256),
                        LastLoginDate = c.DateTime(),
                        RoleId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Pages", "PageGroupId", "dbo.PageGroups");
            DropForeignKey("dbo.PageGroups", "ParentPageGroup_Id", "dbo.PageGroups");
            DropForeignKey("dbo.Comments", "ParentComment_Id", "dbo.Comments");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Pages", new[] { "PageGroupId" });
            DropIndex("dbo.PageGroups", new[] { "ParentPageGroup_Id" });
            DropIndex("dbo.Comments", new[] { "ParentComment_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.RelatedPages");
            DropTable("dbo.Pages");
            DropTable("dbo.PageGroups");
            DropTable("dbo.Comments");
        }
    }
}

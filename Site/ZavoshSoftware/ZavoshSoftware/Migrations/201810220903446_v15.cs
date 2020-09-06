namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v15 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PortfolioGroups",
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
                        ParentId = c.Guid(),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PortfolioGroups", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        UrlParameter = c.String(nullable: false, maxLength: 200),
                        Summery = c.String(maxLength: 500),
                        Body = c.String(nullable: false, storeType: "ntext"),
                        ImageUrl = c.String(maxLength: 500),
                        PortfolioGroupId = c.Guid(),
                        Notation = c.String(),
                        MetaDescription = c.String(maxLength: 350),
                        IsActive = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PortfolioGroups", t => t.PortfolioGroupId)
                .Index(t => t.PortfolioGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Portfolios", "PortfolioGroupId", "dbo.PortfolioGroups");
            DropForeignKey("dbo.PortfolioGroups", "ParentId", "dbo.PortfolioGroups");
            DropIndex("dbo.Portfolios", new[] { "PortfolioGroupId" });
            DropIndex("dbo.PortfolioGroups", new[] { "ParentId" });
            DropTable("dbo.Portfolios");
            DropTable("dbo.PortfolioGroups");
        }
    }
}

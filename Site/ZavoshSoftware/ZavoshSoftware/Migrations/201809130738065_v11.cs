namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Pages", "PositionId", c => c.Guid());
            CreateIndex("dbo.Pages", "PositionId");
            AddForeignKey("dbo.Pages", "PositionId", "dbo.Positions", "Id");
            DropColumn("dbo.Pages", "IsInMenu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pages", "IsInMenu", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Pages", "PositionId", "dbo.Positions");
            DropIndex("dbo.Pages", new[] { "PositionId" });
            DropColumn("dbo.Pages", "PositionId");
            DropTable("dbo.Positions");
        }
    }
}

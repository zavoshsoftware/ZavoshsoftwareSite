namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v07 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.RelatedPages");
        }
        
        public override void Down()
        {
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
            
        }
    }
}

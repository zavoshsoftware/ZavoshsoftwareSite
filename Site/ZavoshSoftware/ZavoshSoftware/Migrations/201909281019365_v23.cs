namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pages", "Page_Id", "dbo.Pages");
            DropIndex("dbo.Pages", new[] { "Page_Id" });
            DropColumn("dbo.Pages", "Page_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pages", "Page_Id", c => c.Guid());
            CreateIndex("dbo.Pages", "Page_Id");
            AddForeignKey("dbo.Pages", "Page_Id", "dbo.Pages", "Id");
        }
    }
}

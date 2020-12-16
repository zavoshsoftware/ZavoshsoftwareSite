namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Response", c => c.String());
            AddColumn("dbo.Comments", "ResponseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "ResponseDate");
            DropColumn("dbo.Comments", "Response");
        }
    }
}

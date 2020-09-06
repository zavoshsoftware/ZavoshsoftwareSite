namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V26 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BlogComments", "Response", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlogComments", "Response", c => c.String(nullable: false, storeType: "ntext"));
        }
    }
}

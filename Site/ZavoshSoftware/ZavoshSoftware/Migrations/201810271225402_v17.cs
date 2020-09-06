namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v17 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Portfolios", "AddressUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Portfolios", "AddressUrl", c => c.Int(nullable: false));
        }
    }
}

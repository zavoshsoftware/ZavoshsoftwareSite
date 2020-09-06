namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Portfolios", "AddressUrl", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Portfolios", "AddressUrl");
        }
    }
}

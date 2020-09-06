namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v18 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Body", c => c.String(nullable: false, storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Body", c => c.String(nullable: false, maxLength: 200));
        }
    }
}

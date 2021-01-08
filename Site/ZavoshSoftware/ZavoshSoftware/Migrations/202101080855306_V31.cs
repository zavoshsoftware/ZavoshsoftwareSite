namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "SummeryInDetail", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "SummeryInDetail");
        }
    }
}

namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "TitleTag", c => c.String(maxLength: 350));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "TitleTag");
        }
    }
}

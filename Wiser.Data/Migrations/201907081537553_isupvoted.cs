namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isupvoted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wisdom", "IsUpvoted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Wisdom", "IsUpvoted");
        }
    }
}

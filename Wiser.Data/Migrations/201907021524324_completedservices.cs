namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class completedservices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Author", "WisdomCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Author", "WisdomCount");
        }
    }
}

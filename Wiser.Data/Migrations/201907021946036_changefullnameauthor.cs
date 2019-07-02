namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changefullnameauthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Author", "FullName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Author", "FullName");
        }
    }
}

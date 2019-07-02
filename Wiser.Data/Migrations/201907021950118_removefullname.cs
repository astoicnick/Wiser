namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removefullname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AlterColumn("dbo.Author", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Author", "FullName", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "FullName");
        }
    }
}

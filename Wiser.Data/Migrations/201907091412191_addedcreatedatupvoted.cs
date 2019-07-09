namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcreatedatupvoted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Upvoted", "CreatedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "VirtueToGiveToday");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "VirtueToGiveToday", c => c.Int(nullable: false));
            DropColumn("dbo.Upvoted", "CreatedAt");
        }
    }
}

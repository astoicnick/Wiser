namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customusercreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Virtue", c => c.Int());
            AddColumn("dbo.AspNetUsers", "VirtueToGiveToday", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "VirtueToGiveToday");
            DropColumn("dbo.AspNetUsers", "Virtue");
        }
    }
}

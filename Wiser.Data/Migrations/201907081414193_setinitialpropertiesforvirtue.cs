namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setinitialpropertiesforvirtue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Virtue", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "VirtueToGiveToday", c => c.Int(nullable: false));
            AlterColumn("dbo.Wisdom", "PostVirtue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Wisdom", "PostVirtue", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "VirtueToGiveToday", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Virtue", c => c.Int());
        }
    }
}

namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationupdatedadmin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Wisdom", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Wisdom", "PostVirtue", c => c.Int(nullable: false));
            AlterColumn("dbo.Wisdom", "Source", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Wisdom", "Source", c => c.String());
            AlterColumn("dbo.Wisdom", "PostVirtue", c => c.Int());
            AlterColumn("dbo.Wisdom", "Content", c => c.String());
        }
    }
}

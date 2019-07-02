namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removerequiredwisdom : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Wisdom", "PostVirtue", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Wisdom", "PostVirtue", c => c.Int(nullable: false));
        }
    }
}

namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requirefkinwisdom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wisdom", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Wisdom", new[] { "UserId" });
            AlterColumn("dbo.Wisdom", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Wisdom", "UserId");
            AddForeignKey("dbo.Wisdom", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wisdom", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Wisdom", new[] { "UserId" });
            AlterColumn("dbo.Wisdom", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Wisdom", "UserId");
            AddForeignKey("dbo.Wisdom", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}

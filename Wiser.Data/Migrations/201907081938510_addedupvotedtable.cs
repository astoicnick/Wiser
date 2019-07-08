namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedupvotedtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Upvoted",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        WisdomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Wisdom", t => t.WisdomId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.WisdomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Upvoted", "WisdomId", "dbo.Wisdom");
            DropForeignKey("dbo.Upvoted", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Upvoted", new[] { "WisdomId" });
            DropIndex("dbo.Upvoted", new[] { "UserId" });
            DropTable("dbo.Upvoted");
        }
    }
}

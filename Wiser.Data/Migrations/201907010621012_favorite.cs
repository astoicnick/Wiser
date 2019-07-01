namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favorite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        Virtue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Favorite",
                c => new
                    {
                        FavoriteId = c.Int(nullable: false, identity: true),
                        WisdomId = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.FavoriteId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Wisdom", t => t.WisdomId, cascadeDelete: true)
                .Index(t => t.WisdomId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Wisdom",
                c => new
                    {
                        WisdomId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Content = c.String(),
                        WisdomGenre = c.Int(nullable: false),
                        PostVirtue = c.Int(),
                        Source = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WisdomId)
                .ForeignKey("dbo.Author", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorite", "WisdomId", "dbo.Wisdom");
            DropForeignKey("dbo.Wisdom", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.Favorite", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Wisdom", new[] { "AuthorId" });
            DropIndex("dbo.Favorite", new[] { "User_Id" });
            DropIndex("dbo.Favorite", new[] { "WisdomId" });
            DropTable("dbo.Wisdom");
            DropTable("dbo.Favorite");
            DropTable("dbo.Author");
        }
    }
}

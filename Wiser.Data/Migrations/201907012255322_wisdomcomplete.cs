namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wisdomcomplete : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Favorite", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Wisdom", name: "Author_AuthorId", newName: "AuthorId");
            RenameIndex(table: "dbo.Favorite", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.Wisdom", name: "IX_Author_AuthorId", newName: "IX_AuthorId");
            AddColumn("dbo.Wisdom", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Wisdom", "UserId");
            AddForeignKey("dbo.Wisdom", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Wisdom", "OwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wisdom", "OwnerId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Wisdom", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Wisdom", new[] { "UserId" });
            DropColumn("dbo.Wisdom", "UserId");
            RenameIndex(table: "dbo.Wisdom", name: "IX_AuthorId", newName: "IX_Author_AuthorId");
            RenameIndex(table: "dbo.Favorite", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Wisdom", name: "AuthorId", newName: "Author_AuthorId");
            RenameColumn(table: "dbo.Favorite", name: "UserId", newName: "User_Id");
        }
    }
}

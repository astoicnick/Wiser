namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmforeignkeywisdom : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Wisdom", name: "AuthorId", newName: "Author_AuthorId");
            RenameIndex(table: "dbo.Wisdom", name: "IX_AuthorId", newName: "IX_Author_AuthorId");
            DropColumn("dbo.Author", "CreatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Author", "CreatedAt", c => c.DateTime(nullable: false));
            RenameIndex(table: "dbo.Wisdom", name: "IX_Author_AuthorId", newName: "IX_AuthorId");
            RenameColumn(table: "dbo.Wisdom", name: "Author_AuthorId", newName: "AuthorId");
        }
    }
}

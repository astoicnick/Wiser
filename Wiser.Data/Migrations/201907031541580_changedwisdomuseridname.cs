namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedwisdomuseridname : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Wisdom", name: "Id", newName: "UserId");
            RenameIndex(table: "dbo.Wisdom", name: "IX_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Wisdom", name: "IX_UserId", newName: "IX_Id");
            RenameColumn(table: "dbo.Wisdom", name: "UserId", newName: "Id");
        }
    }
}

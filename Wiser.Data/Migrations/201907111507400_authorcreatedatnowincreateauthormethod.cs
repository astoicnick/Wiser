namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authorcreatedatnowincreateauthormethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Author", "CreatedAt", c => c.DateTime(nullable: false));
        }
        public override void Down()
        {
            DropColumn("dbo.Author", "CreatedAt");
        }
    }
}

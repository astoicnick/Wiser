namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedsomerequiredauthor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Author", "Virtue", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Author", "Virtue", c => c.Int(nullable: false));
        }
    }
}

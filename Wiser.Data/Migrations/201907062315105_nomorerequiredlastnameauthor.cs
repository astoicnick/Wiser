namespace Wiser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nomorerequiredlastnameauthor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Author", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Author", "LastName", c => c.String(nullable: false));
        }
    }
}

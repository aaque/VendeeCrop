namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserModels", "Type", c => c.String(maxLength: 50));
            AlterColumn("dbo.UserModels", "StoreName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserModels", "StoreName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.UserModels", "Type", c => c.String(nullable: false, maxLength: 50));
        }
    }
}

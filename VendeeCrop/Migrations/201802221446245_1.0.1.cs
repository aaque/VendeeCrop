namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _101 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NotificationModels", "Message", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NotificationModels", "Message", c => c.Int(nullable: false));
        }
    }
}

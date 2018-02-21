namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _71 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostImageModels", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostImageModels", "Image", c => c.Binary());
        }
    }
}

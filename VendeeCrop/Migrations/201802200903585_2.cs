namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CropPostModels", "PostType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CropPostModels", "PostType", c => c.String(nullable: false));
        }
    }
}

namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1224pm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CropPostModels", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CropPostModels", "Created");
        }
    }
}

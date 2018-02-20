namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CropPostDetailModels", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CropPostModels", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CropPostModels", "Type");
            DropColumn("dbo.CropPostDetailModels", "Price");
        }
    }
}

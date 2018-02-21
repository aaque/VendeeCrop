namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _73 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PostImageModels", newName: "CropPostImageModels");
            DropPrimaryKey("dbo.CropPostImageModels");
            AddColumn("dbo.CropPostImageModels", "ImageId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CropPostImageModels", "Created", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.CropPostImageModels", "ImageId");
            DropColumn("dbo.CropPostImageModels", "PostImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CropPostImageModels", "PostImageId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.CropPostImageModels");
            DropColumn("dbo.CropPostImageModels", "Created");
            DropColumn("dbo.CropPostImageModels", "ImageId");
            AddPrimaryKey("dbo.CropPostImageModels", "PostImageId");
            RenameTable(name: "dbo.CropPostImageModels", newName: "PostImageModels");
        }
    }
}

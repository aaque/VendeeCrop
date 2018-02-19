namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _021918_0416PM_VendeeCrop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CropPostDetailModels", "CropTypeId", "dbo.CropTypeModels");
            DropForeignKey("dbo.CropPostDetailModels", "CropPostId", "dbo.CropPostModels");
            DropForeignKey("dbo.PostImageModels", "CropPostId", "dbo.CropPostModels");
            DropIndex("dbo.CropPostDetailModels", new[] { "CropTypeId" });
            DropPrimaryKey("dbo.CropPostDetailModels");
            DropPrimaryKey("dbo.CropPostModels");
            AddColumn("dbo.CropPostDetailModels", "Details", c => c.String());
            AddColumn("dbo.CropPostDetailModels", "CropId", c => c.Int(nullable: false));
            AddColumn("dbo.CropPostDetailModels", "Status", c => c.String());
            AddColumn("dbo.CropPostDetailModels", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.CropPostModels", "FarmerModelId", c => c.Int(nullable: false));
            AlterColumn("dbo.CropPostDetailModels", "CropPostDetailId", c => c.Int(nullable: false));
            AlterColumn("dbo.CropPostModels", "CropPostId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CropPostDetailModels", "CropPostDetailId");
            AddPrimaryKey("dbo.CropPostModels", "CropPostId");
            CreateIndex("dbo.CropPostDetailModels", "CropPostDetailId");
            CreateIndex("dbo.CropPostModels", "CropPostId");
            AddForeignKey("dbo.CropPostDetailModels", "CropPostDetailId", "dbo.CropModels", "Id");
            AddForeignKey("dbo.CropPostModels", "CropPostId", "dbo.FarmerModels", "Id");
            AddForeignKey("dbo.CropPostDetailModels", "CropPostId", "dbo.CropPostModels", "CropPostId", cascadeDelete: true);
            AddForeignKey("dbo.PostImageModels", "CropPostId", "dbo.CropPostModels", "CropPostId", cascadeDelete: true);
            DropColumn("dbo.CropPostDetailModels", "CropTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CropPostDetailModels", "CropTypeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PostImageModels", "CropPostId", "dbo.CropPostModels");
            DropForeignKey("dbo.CropPostDetailModels", "CropPostId", "dbo.CropPostModels");
            DropForeignKey("dbo.CropPostModels", "CropPostId", "dbo.FarmerModels");
            DropForeignKey("dbo.CropPostDetailModels", "CropPostDetailId", "dbo.CropModels");
            DropIndex("dbo.CropPostModels", new[] { "CropPostId" });
            DropIndex("dbo.CropPostDetailModels", new[] { "CropPostDetailId" });
            DropPrimaryKey("dbo.CropPostModels");
            DropPrimaryKey("dbo.CropPostDetailModels");
            AlterColumn("dbo.CropPostModels", "CropPostId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CropPostDetailModels", "CropPostDetailId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.CropPostModels", "FarmerModelId");
            DropColumn("dbo.CropPostDetailModels", "Created");
            DropColumn("dbo.CropPostDetailModels", "Status");
            DropColumn("dbo.CropPostDetailModels", "CropId");
            DropColumn("dbo.CropPostDetailModels", "Details");
            AddPrimaryKey("dbo.CropPostModels", "CropPostId");
            AddPrimaryKey("dbo.CropPostDetailModels", "CropPostDetailId");
            CreateIndex("dbo.CropPostDetailModels", "CropTypeId");
            AddForeignKey("dbo.PostImageModels", "CropPostId", "dbo.CropPostModels", "CropPostId", cascadeDelete: true);
            AddForeignKey("dbo.CropPostDetailModels", "CropPostId", "dbo.CropPostModels", "CropPostId", cascadeDelete: true);
            AddForeignKey("dbo.CropPostDetailModels", "CropTypeId", "dbo.CropTypeModels", "CropTypeId", cascadeDelete: true);
        }
    }
}

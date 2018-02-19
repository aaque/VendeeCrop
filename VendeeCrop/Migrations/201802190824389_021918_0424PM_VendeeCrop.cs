namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _021918_0424PM_VendeeCrop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CropPostDetailModels", "CropPostDetailId", "dbo.CropModels");
            DropForeignKey("dbo.CropPostModels", "CropPostId", "dbo.FarmerModels");
            DropForeignKey("dbo.CropPostDetailModels", "CropPostId", "dbo.CropPostModels");
            DropForeignKey("dbo.PostImageModels", "CropPostId", "dbo.CropPostModels");
            DropColumn("dbo.CropPostDetailModels", "CropId");
            DropColumn("dbo.CropPostModels", "FarmerModelId");
            RenameColumn(table: "dbo.CropPostDetailModels", name: "CropPostDetailId", newName: "CropId");
            RenameColumn(table: "dbo.CropPostModels", name: "CropPostId", newName: "FarmerModelId");
            RenameIndex(table: "dbo.CropPostDetailModels", name: "IX_CropPostDetailId", newName: "IX_CropId");
            RenameIndex(table: "dbo.CropPostModels", name: "IX_CropPostId", newName: "IX_FarmerModelId");
            DropPrimaryKey("dbo.CropPostDetailModels");
            DropPrimaryKey("dbo.CropPostModels");
            AlterColumn("dbo.CropPostDetailModels", "CropPostDetailId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CropPostModels", "CropPostId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CropPostDetailModels", "CropPostDetailId");
            AddPrimaryKey("dbo.CropPostModels", "CropPostId");
            AddForeignKey("dbo.CropPostDetailModels", "CropId", "dbo.CropModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CropPostModels", "FarmerModelId", "dbo.FarmerModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CropPostDetailModels", "CropPostId", "dbo.CropPostModels", "CropPostId", cascadeDelete: true);
            AddForeignKey("dbo.PostImageModels", "CropPostId", "dbo.CropPostModels", "CropPostId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostImageModels", "CropPostId", "dbo.CropPostModels");
            DropForeignKey("dbo.CropPostDetailModels", "CropPostId", "dbo.CropPostModels");
            DropForeignKey("dbo.CropPostModels", "FarmerModelId", "dbo.FarmerModels");
            DropForeignKey("dbo.CropPostDetailModels", "CropId", "dbo.CropModels");
            DropPrimaryKey("dbo.CropPostModels");
            DropPrimaryKey("dbo.CropPostDetailModels");
            AlterColumn("dbo.CropPostModels", "CropPostId", c => c.Int(nullable: false));
            AlterColumn("dbo.CropPostDetailModels", "CropPostDetailId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CropPostModels", "CropPostId");
            AddPrimaryKey("dbo.CropPostDetailModels", "CropPostDetailId");
            RenameIndex(table: "dbo.CropPostModels", name: "IX_FarmerModelId", newName: "IX_CropPostId");
            RenameIndex(table: "dbo.CropPostDetailModels", name: "IX_CropId", newName: "IX_CropPostDetailId");
            RenameColumn(table: "dbo.CropPostModels", name: "FarmerModelId", newName: "CropPostId");
            RenameColumn(table: "dbo.CropPostDetailModels", name: "CropId", newName: "CropPostDetailId");
            AddColumn("dbo.CropPostModels", "FarmerModelId", c => c.Int(nullable: false));
            AddColumn("dbo.CropPostDetailModels", "CropId", c => c.Int(nullable: false));
            AddForeignKey("dbo.PostImageModels", "CropPostId", "dbo.CropPostModels", "CropPostId", cascadeDelete: true);
            AddForeignKey("dbo.CropPostDetailModels", "CropPostId", "dbo.CropPostModels", "CropPostId", cascadeDelete: true);
            AddForeignKey("dbo.CropPostModels", "CropPostId", "dbo.FarmerModels", "Id");
            AddForeignKey("dbo.CropPostDetailModels", "CropPostDetailId", "dbo.CropModels", "Id");
        }
    }
}

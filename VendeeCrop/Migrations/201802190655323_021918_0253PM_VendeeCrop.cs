namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _021918_0253PM_VendeeCrop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CropPostDetailModels",
                c => new
                    {
                        CropPostDetailId = c.Int(nullable: false, identity: true),
                        CropPostId = c.Int(nullable: false),
                        CropTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CropPostDetailId)
                .ForeignKey("dbo.CropPostModels", t => t.CropPostId, cascadeDelete: true)
                .ForeignKey("dbo.CropTypeModels", t => t.CropTypeId, cascadeDelete: true)
                .Index(t => t.CropPostId)
                .Index(t => t.CropTypeId);
            
            CreateTable(
                "dbo.CropPostModels",
                c => new
                    {
                        CropPostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CropPostId);
            
            CreateTable(
                "dbo.PostImageModels",
                c => new
                    {
                        PostImageId = c.String(nullable: false, maxLength: 128),
                        CropPostId = c.Int(nullable: false),
                        ImageName = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.PostImageId)
                .ForeignKey("dbo.CropPostModels", t => t.CropPostId, cascadeDelete: true)
                .Index(t => t.CropPostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CropPostDetailModels", "CropTypeId", "dbo.CropTypeModels");
            DropForeignKey("dbo.PostImageModels", "CropPostId", "dbo.CropPostModels");
            DropForeignKey("dbo.CropPostDetailModels", "CropPostId", "dbo.CropPostModels");
            DropIndex("dbo.PostImageModels", new[] { "CropPostId" });
            DropIndex("dbo.CropPostDetailModels", new[] { "CropTypeId" });
            DropIndex("dbo.CropPostDetailModels", new[] { "CropPostId" });
            DropTable("dbo.PostImageModels");
            DropTable("dbo.CropPostModels");
            DropTable("dbo.CropPostDetailModels");
        }
    }
}

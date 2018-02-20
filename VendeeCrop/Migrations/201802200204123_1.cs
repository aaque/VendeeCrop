namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CropModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        CropTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CropTypeModels", t => t.CropTypeId, cascadeDelete: true)
                .Index(t => t.CropTypeId);
            
            CreateTable(
                "dbo.CropTypeModels",
                c => new
                    {
                        CropTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CropTypeId);
            
            CreateTable(
                "dbo.CropPostDetailModels",
                c => new
                    {
                        CropPostDetailId = c.Int(nullable: false, identity: true),
                        Details = c.String(),
                        CropPostId = c.Int(nullable: false),
                        CropId = c.Int(nullable: false),
                        Status = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CropPostDetailId)
                .ForeignKey("dbo.CropModels", t => t.CropId, cascadeDelete: true)
                .ForeignKey("dbo.CropPostModels", t => t.CropPostId, cascadeDelete: true)
                .Index(t => t.CropPostId)
                .Index(t => t.CropId);
            
            CreateTable(
                "dbo.CropPostModels",
                c => new
                    {
                        CropPostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        FarmerModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CropPostId)
                .ForeignKey("dbo.FarmerModels", t => t.FarmerModelId, cascadeDelete: true)
                .Index(t => t.FarmerModelId);
            
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
            DropForeignKey("dbo.PostImageModels", "CropPostId", "dbo.CropPostModels");
            DropForeignKey("dbo.CropPostModels", "FarmerModelId", "dbo.FarmerModels");
            DropForeignKey("dbo.CropPostDetailModels", "CropPostId", "dbo.CropPostModels");
            DropForeignKey("dbo.CropPostDetailModels", "CropId", "dbo.CropModels");
            DropForeignKey("dbo.CropModels", "CropTypeId", "dbo.CropTypeModels");
            DropIndex("dbo.PostImageModels", new[] { "CropPostId" });
            DropIndex("dbo.CropPostModels", new[] { "FarmerModelId" });
            DropIndex("dbo.CropPostDetailModels", new[] { "CropId" });
            DropIndex("dbo.CropPostDetailModels", new[] { "CropPostId" });
            DropIndex("dbo.CropModels", new[] { "CropTypeId" });
            DropTable("dbo.PostImageModels");
            DropTable("dbo.CropPostModels");
            DropTable("dbo.CropPostDetailModels");
            DropTable("dbo.CropTypeModels");
            DropTable("dbo.CropModels");
        }
    }
}

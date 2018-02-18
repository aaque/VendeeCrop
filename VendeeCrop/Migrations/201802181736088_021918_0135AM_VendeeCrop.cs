namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _021918_0135AM_VendeeCrop : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CropModels", "CropTypeId", "dbo.CropTypeModels");
            DropIndex("dbo.CropModels", new[] { "CropTypeId" });
            DropTable("dbo.CropTypeModels");
            DropTable("dbo.CropModels");
        }
    }
}

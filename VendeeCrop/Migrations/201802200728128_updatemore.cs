namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemore : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CropPostModels", "FarmerModelId", "dbo.FarmerModels");
            DropIndex("dbo.CropPostModels", new[] { "FarmerModelId" });
            RenameColumn(table: "dbo.CropPostModels", name: "FarmerModelId", newName: "FarmerModel_Id");
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        StoreName = c.String(nullable: false, maxLength: 255),
                        Address = c.String(nullable: false, maxLength: 255),
                        ImagePath = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CropPostModels", "UserModelId", c => c.Int(nullable: false));
            AlterColumn("dbo.CropPostModels", "FarmerModel_Id", c => c.Int());
            CreateIndex("dbo.CropPostModels", "UserModelId");
            CreateIndex("dbo.CropPostModels", "FarmerModel_Id");
            AddForeignKey("dbo.CropPostModels", "UserModelId", "dbo.UserModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CropPostModels", "FarmerModel_Id", "dbo.FarmerModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CropPostModels", "FarmerModel_Id", "dbo.FarmerModels");
            DropForeignKey("dbo.CropPostModels", "UserModelId", "dbo.UserModels");
            DropIndex("dbo.CropPostModels", new[] { "FarmerModel_Id" });
            DropIndex("dbo.CropPostModels", new[] { "UserModelId" });
            AlterColumn("dbo.CropPostModels", "FarmerModel_Id", c => c.Int(nullable: false));
            DropColumn("dbo.CropPostModels", "UserModelId");
            DropTable("dbo.UserModels");
            RenameColumn(table: "dbo.CropPostModels", name: "FarmerModel_Id", newName: "FarmerModelId");
            CreateIndex("dbo.CropPostModels", "FarmerModelId");
            AddForeignKey("dbo.CropPostModels", "FarmerModelId", "dbo.FarmerModels", "Id", cascadeDelete: true);
        }
    }
}

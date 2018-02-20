namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyerModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        StoreName = c.String(nullable: false, maxLength: 255),
                        BusinessAddress = c.String(nullable: false, maxLength: 255),
                        ImagePath = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                        PostType = c.String(nullable: false),
                        UserModelId = c.Int(nullable: false),
                        FarmerModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.CropPostId)
                .ForeignKey("dbo.UserModels", t => t.UserModelId, cascadeDelete: true)
                .ForeignKey("dbo.FarmerModels", t => t.FarmerModel_Id)
                .Index(t => t.UserModelId)
                .Index(t => t.FarmerModel_Id);
            
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
            
            CreateTable(
                "dbo.FarmerModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        Address = c.String(nullable: false, maxLength: 255),
                        ImagePath = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CropPostModels", "FarmerModel_Id", "dbo.FarmerModels");
            DropForeignKey("dbo.CropPostModels", "UserModelId", "dbo.UserModels");
            DropForeignKey("dbo.PostImageModels", "CropPostId", "dbo.CropPostModels");
            DropForeignKey("dbo.CropPostDetailModels", "CropPostId", "dbo.CropPostModels");
            DropForeignKey("dbo.CropPostDetailModels", "CropId", "dbo.CropModels");
            DropForeignKey("dbo.CropModels", "CropTypeId", "dbo.CropTypeModels");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PostImageModels", new[] { "CropPostId" });
            DropIndex("dbo.CropPostModels", new[] { "FarmerModel_Id" });
            DropIndex("dbo.CropPostModels", new[] { "UserModelId" });
            DropIndex("dbo.CropPostDetailModels", new[] { "CropId" });
            DropIndex("dbo.CropPostDetailModels", new[] { "CropPostId" });
            DropIndex("dbo.CropModels", new[] { "CropTypeId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.FarmerModels");
            DropTable("dbo.UserModels");
            DropTable("dbo.PostImageModels");
            DropTable("dbo.CropPostModels");
            DropTable("dbo.CropPostDetailModels");
            DropTable("dbo.CropTypeModels");
            DropTable("dbo.CropModels");
            DropTable("dbo.BuyerModels");
        }
    }
}

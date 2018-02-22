namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _101 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MessageModels", "UserId", "dbo.UserModels");
            DropIndex("dbo.MessageModels", new[] { "UserId" });
            RenameColumn(table: "dbo.MessageModels", name: "UserId", newName: "FromUserId");
            AddColumn("dbo.MessageModels", "ToUserID", c => c.Int());
            AlterColumn("dbo.MessageModels", "FromUserId", c => c.Int());
            CreateIndex("dbo.MessageModels", "FromUserId");
            CreateIndex("dbo.MessageModels", "ToUserID");
            AddForeignKey("dbo.MessageModels", "ToUserID", "dbo.UserModels", "Id");
            AddForeignKey("dbo.MessageModels", "FromUserId", "dbo.UserModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageModels", "FromUserId", "dbo.UserModels");
            DropForeignKey("dbo.MessageModels", "ToUserID", "dbo.UserModels");
            DropIndex("dbo.MessageModels", new[] { "ToUserID" });
            DropIndex("dbo.MessageModels", new[] { "FromUserId" });
            AlterColumn("dbo.MessageModels", "FromUserId", c => c.Int(nullable: false));
            DropColumn("dbo.MessageModels", "ToUserID");
            RenameColumn(table: "dbo.MessageModels", name: "FromUserId", newName: "UserId");
            CreateIndex("dbo.MessageModels", "UserId");
            AddForeignKey("dbo.MessageModels", "UserId", "dbo.UserModels", "Id", cascadeDelete: true);
        }
    }
}

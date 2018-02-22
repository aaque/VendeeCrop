namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _102 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MessageModels", "ToUserID", "dbo.UserModels");
            DropIndex("dbo.MessageModels", new[] { "ToUserID" });
            AlterColumn("dbo.MessageModels", "ToUserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MessageModels", "ToUserID", c => c.Int());
            CreateIndex("dbo.MessageModels", "ToUserID");
            AddForeignKey("dbo.MessageModels", "ToUserID", "dbo.UserModels", "Id");
        }
    }
}

namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _100 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageModels",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Message = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.UserModels", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.NotificationModels",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        TypeTo = c.String(),
                        ToId = c.Int(nullable: false),
                        MessageType = c.String(),
                        Message = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        UserNotificationId = c.Int(nullable: false, identity: true),
                        NotificationId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsUnread = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserNotificationId)
                .ForeignKey("dbo.NotificationModels", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.UserModels", t => t.UserId, cascadeDelete: true)
                .Index(t => t.NotificationId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.UserModels");
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.NotificationModels");
            DropForeignKey("dbo.MessageModels", "UserId", "dbo.UserModels");
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            DropIndex("dbo.MessageModels", new[] { "UserId" });
            DropTable("dbo.UserNotifications");
            DropTable("dbo.NotificationModels");
            DropTable("dbo.MessageModels");
        }
    }
}

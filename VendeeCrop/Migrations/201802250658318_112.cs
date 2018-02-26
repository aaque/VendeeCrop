namespace VendeeCrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _112 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserMessages",
                c => new
                    {
                        UserMessageId = c.Int(nullable: false, identity: true),
                        MessageId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsUnread = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserMessageId)
                .ForeignKey("dbo.MessageModels", t => t.MessageId, cascadeDelete: true)
                .ForeignKey("dbo.UserModels", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MessageId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMessages", "UserId", "dbo.UserModels");
            DropForeignKey("dbo.UserMessages", "MessageId", "dbo.MessageModels");
            DropIndex("dbo.UserMessages", new[] { "UserId" });
            DropIndex("dbo.UserMessages", new[] { "MessageId" });
            DropTable("dbo.UserMessages");
        }
    }
}

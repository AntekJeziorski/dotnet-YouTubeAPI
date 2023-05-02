namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tracks", "AuthorID", "dbo.Authors");
            DropForeignKey("dbo.AuthorsHistories", "AuthorID", "dbo.Authors");
            DropIndex("dbo.AuthorsHistories", new[] { "AuthorID" });
            DropIndex("dbo.Tracks", new[] { "AuthorID" });
            RenameColumn(table: "dbo.AuthorsHistories", name: "AuthorID", newName: "ChannelId");
            DropPrimaryKey("dbo.Authors");
            AddColumn("dbo.Authors", "ChannelId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Authors", "ChannleTitle", c => c.String());
            AddColumn("dbo.Authors", "ChannleDescription", c => c.String());
            AddColumn("dbo.Authors", "ThumbnailMedium", c => c.String());
            AddColumn("dbo.AuthorsHistories", "VideoCount", c => c.Int(nullable: false));
            AlterColumn("dbo.AuthorsHistories", "ChannelId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Authors", "ChannelId");
            CreateIndex("dbo.AuthorsHistories", "ChannelId");
            AddForeignKey("dbo.AuthorsHistories", "ChannelId", "dbo.Authors", "ChannelId");
            DropColumn("dbo.Authors", "ID");
            DropColumn("dbo.Authors", "Nickname");
            DropColumn("dbo.Authors", "YtChannelID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Authors", "YtChannelID", c => c.String());
            AddColumn("dbo.Authors", "Nickname", c => c.String());
            AddColumn("dbo.Authors", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.AuthorsHistories", "ChannelId", "dbo.Authors");
            DropIndex("dbo.AuthorsHistories", new[] { "ChannelId" });
            DropPrimaryKey("dbo.Authors");
            AlterColumn("dbo.AuthorsHistories", "ChannelId", c => c.Int(nullable: false));
            DropColumn("dbo.AuthorsHistories", "VideoCount");
            DropColumn("dbo.Authors", "ThumbnailMedium");
            DropColumn("dbo.Authors", "ChannleDescription");
            DropColumn("dbo.Authors", "ChannleTitle");
            DropColumn("dbo.Authors", "ChannelId");
            AddPrimaryKey("dbo.Authors", "ID");
            RenameColumn(table: "dbo.AuthorsHistories", name: "ChannelId", newName: "AuthorID");
            CreateIndex("dbo.Tracks", "AuthorID");
            CreateIndex("dbo.AuthorsHistories", "AuthorID");
            AddForeignKey("dbo.AuthorsHistories", "AuthorID", "dbo.Authors", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Tracks", "AuthorID", "dbo.Authors", "ID", cascadeDelete: true);
        }
    }
}

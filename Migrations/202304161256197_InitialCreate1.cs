namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TracksHistories", "TrackID", "dbo.Tracks");
            DropIndex("dbo.TracksHistories", new[] { "TrackID" });
            DropPrimaryKey("dbo.Tracks");
            AddColumn("dbo.Tracks", "VideoId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Tracks", "Title", c => c.String());
            AddColumn("dbo.Tracks", "ChannelTitle", c => c.String());
            AddColumn("dbo.Tracks", "ChannelId", c => c.String());
            AlterColumn("dbo.TracksHistories", "TrackID", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Tracks", "VideoId");
            CreateIndex("dbo.TracksHistories", "TrackID");
            AddForeignKey("dbo.TracksHistories", "TrackID", "dbo.Tracks", "VideoId");
            DropColumn("dbo.Tracks", "ID");
            DropColumn("dbo.Tracks", "YtClipID");
            DropColumn("dbo.Tracks", "ReleaseDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tracks", "ReleaseDate", c => c.String());
            AddColumn("dbo.Tracks", "YtClipID", c => c.String());
            AddColumn("dbo.Tracks", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.TracksHistories", "TrackID", "dbo.Tracks");
            DropIndex("dbo.TracksHistories", new[] { "TrackID" });
            DropPrimaryKey("dbo.Tracks");
            AlterColumn("dbo.TracksHistories", "TrackID", c => c.Int(nullable: false));
            DropColumn("dbo.Tracks", "ChannelId");
            DropColumn("dbo.Tracks", "ChannelTitle");
            DropColumn("dbo.Tracks", "Title");
            DropColumn("dbo.Tracks", "VideoId");
            AddPrimaryKey("dbo.Tracks", "ID");
            CreateIndex("dbo.TracksHistories", "TrackID");
            AddForeignKey("dbo.TracksHistories", "TrackID", "dbo.Tracks", "ID", cascadeDelete: true);
        }
    }
}

namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nickname = c.String(),
                        YtChannelID = c.String(),
                        JoiningDate = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AuthorsHistories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuthorID = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        SubCount = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Authors", t => t.AuthorID, cascadeDelete: true)
                .Index(t => t.AuthorID);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuthorID = c.Int(nullable: false),
                        YtClipID = c.String(),
                        ReleaseDate = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Authors", t => t.AuthorID, cascadeDelete: true)
                .Index(t => t.AuthorID);
            
            CreateTable(
                "dbo.TracksHistories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TrackID = c.Int(nullable: false),
                        LikeCount = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        CommentCount = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tracks", t => t.TrackID, cascadeDelete: true)
                .Index(t => t.TrackID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TracksHistories", "TrackID", "dbo.Tracks");
            DropForeignKey("dbo.Tracks", "AuthorID", "dbo.Authors");
            DropForeignKey("dbo.AuthorsHistories", "AuthorID", "dbo.Authors");
            DropIndex("dbo.TracksHistories", new[] { "TrackID" });
            DropIndex("dbo.Tracks", new[] { "AuthorID" });
            DropIndex("dbo.AuthorsHistories", new[] { "AuthorID" });
            DropTable("dbo.TracksHistories");
            DropTable("dbo.Tracks");
            DropTable("dbo.AuthorsHistories");
            DropTable("dbo.Authors");
        }
    }
}

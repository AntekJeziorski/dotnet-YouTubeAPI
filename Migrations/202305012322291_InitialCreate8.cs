namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate8 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TracksHistories", name: "TrackID", newName: "VideoId");
            RenameIndex(table: "dbo.TracksHistories", name: "IX_TrackID", newName: "IX_VideoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TracksHistories", name: "IX_VideoId", newName: "IX_TrackID");
            RenameColumn(table: "dbo.TracksHistories", name: "VideoId", newName: "TrackID");
        }
    }
}

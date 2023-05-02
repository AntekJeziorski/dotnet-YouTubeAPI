namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "Description", c => c.String());
            AddColumn("dbo.Tracks", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tracks", "ThumbnailMedium", c => c.String());
            DropColumn("dbo.Tracks", "AuthorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tracks", "AuthorID", c => c.Int(nullable: false));
            DropColumn("dbo.Tracks", "ThumbnailMedium");
            DropColumn("dbo.Tracks", "ReleaseDate");
            DropColumn("dbo.Tracks", "Description");
        }
    }
}

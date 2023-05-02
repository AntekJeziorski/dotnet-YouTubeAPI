namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuthorsHistories", "ViewCount", c => c.Int(nullable: false));
            AddColumn("dbo.AuthorsHistories", "SubCount", c => c.Int(nullable: false));
            AddColumn("dbo.AuthorsHistories", "VideoCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuthorsHistories", "VideoCount");
            DropColumn("dbo.AuthorsHistories", "SubCount");
            DropColumn("dbo.AuthorsHistories", "ViewCount");
        }
    }
}

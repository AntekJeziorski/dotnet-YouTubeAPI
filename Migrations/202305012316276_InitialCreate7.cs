namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AuthorsHistories", "ViewCount");
            DropColumn("dbo.AuthorsHistories", "SubCount");
            DropColumn("dbo.AuthorsHistories", "VideoCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuthorsHistories", "VideoCount", c => c.Int(nullable: false));
            AddColumn("dbo.AuthorsHistories", "SubCount", c => c.Int(nullable: false));
            AddColumn("dbo.AuthorsHistories", "ViewCount", c => c.Int(nullable: false));
        }
    }
}

namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AuthorsHistories", "ViewCount", c => c.Long(nullable: false));
            AlterColumn("dbo.AuthorsHistories", "SubCount", c => c.Long(nullable: false));
            AlterColumn("dbo.AuthorsHistories", "VideoCount", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AuthorsHistories", "VideoCount", c => c.Int(nullable: false));
            AlterColumn("dbo.AuthorsHistories", "SubCount", c => c.Int(nullable: false));
            AlterColumn("dbo.AuthorsHistories", "ViewCount", c => c.Int(nullable: false));
        }
    }
}

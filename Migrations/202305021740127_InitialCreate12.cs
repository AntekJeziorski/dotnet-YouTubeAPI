namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TracksHistories", "LikeCount", c => c.Long(nullable: false));
            AlterColumn("dbo.TracksHistories", "ViewCount", c => c.Long(nullable: false));
            AlterColumn("dbo.TracksHistories", "CommentCount", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TracksHistories", "CommentCount", c => c.Int(nullable: false));
            AlterColumn("dbo.TracksHistories", "ViewCount", c => c.Int(nullable: false));
            AlterColumn("dbo.TracksHistories", "LikeCount", c => c.Int(nullable: false));
        }
    }
}

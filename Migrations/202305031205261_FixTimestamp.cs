namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTimestamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuthorsHistories", "AddTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.TracksHistories", "AddTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.AuthorsHistories", "Timestamp");
            DropColumn("dbo.TracksHistories", "Timestamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TracksHistories", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.AuthorsHistories", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            DropColumn("dbo.TracksHistories", "AddTime");
            DropColumn("dbo.AuthorsHistories", "AddTime");
        }
    }
}

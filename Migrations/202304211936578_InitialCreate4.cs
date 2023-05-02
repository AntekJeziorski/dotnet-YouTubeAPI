namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "ChannelTitle", c => c.String());
            AddColumn("dbo.Authors", "ChannelDescription", c => c.String());
            DropColumn("dbo.Authors", "ChannleTitle");
            DropColumn("dbo.Authors", "ChannleDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Authors", "ChannleDescription", c => c.String());
            AddColumn("dbo.Authors", "ChannleTitle", c => c.String());
            DropColumn("dbo.Authors", "ChannelDescription");
            DropColumn("dbo.Authors", "ChannelTitle");
        }
    }
}

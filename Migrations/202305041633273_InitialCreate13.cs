namespace dotnet_YouTubeAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "SubscribeTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tracks", "SubscribeTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "SubscribeTime");
            DropColumn("dbo.Authors", "SubscribeTime");
        }
    }
}

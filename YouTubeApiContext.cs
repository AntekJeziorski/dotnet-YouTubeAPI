using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Collections.ObjectModel;

namespace YouTubeAPI
{
    public class YouTubeApiContext : DbContext
    {
        // Your context has been configured to use a 'YouTubeApiContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'dotnet_YouTubeAPI.YouTubeApiContext' database on your LocalDb instance. 
        public YouTubeApiContext()
            : base("name=YouTubeApiContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<TracksHistory> TrackHist { get; set; }
        public DbSet<AuthorsHistory> AuthorsHist { get; set; }
    }
    public class YouTubeDbInitializer : DropCreateDatabaseAlways<YouTubeApiContext>
    {
        protected override void Seed(YouTubeApiContext context)
        {
            var authors = new List<Author>
            { 
                new Author() { ID = 1, Nickname = "Klawiatur", YtChannelID = "irueope804", JoiningDate = "04/23/22 04:34:22" },
            };
            authors.ForEach(c => context.Authors.Add(c));
            context.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YouTubeAPI
{
    public class YouTubeApiContext : DbContext
    {
        // Your context has been configured to use a 'YouTubeApiContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'dotnet_YouTubeAPI.YouTubeApiContext' database on your LocalDb instance. 
        public YouTubeApiContext() : base("name=YouTubeApiContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new YouTubeApiDbInitializer());
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<TracksHistory> TracksHistory { get; set; }
        public DbSet<AuthorsHistory> AuthorsHistory { get; set; }

        public void addNewAuthor(Author author)
        {
            using(var context = new YouTubeApiContext())
            {
                context.Authors.Add(author);
                context.SaveChanges();
            }
        }

    }
    public class YouTubeApiDbInitializer : DropCreateDatabaseAlways<YouTubeApiContext>
    {
        protected override void Seed(YouTubeApiContext context)
        {

            IList<Author> defaultAuthors = new List<Author>();

            defaultAuthors.Add(new Author() { ID = 1, Nickname = "Klawiatur", YtChannelID = "irueope804", JoiningDate = "04/23/22 04:34:22" });
            defaultAuthors.Add(new Author() { ID = 2, Nickname = "Klawiatur", YtChannelID = "irueope804", JoiningDate = "04/23/22 04:34:22" });
            defaultAuthors.Add(new Author() { ID = 3, Nickname = "Klawiatur", YtChannelID = "irueope804", JoiningDate = "04/23/22 04:34:22" });

            context.Authors.AddRange(defaultAuthors);

            base.Seed(context);
        }
    }
}
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
            IList<Track> defaultTracks = new List<Track>();


            //defaultAuthors.Add(new Author() { ID = 1, Nickname = "Kuba Klawiter", YtChannelID = "UCLr4hMhk_2KE0GUBSBrspGA", JoiningDate = "2013-10-01T00:23:46Z" });
            //defaultAuthors.Add(new Author() { ID = 2, Nickname = "Linus Tech Tips", YtChannelID = "UCXuqSBlHAE6Xw-yeJA0Tunw", JoiningDate = "2008-11-25T00:46:52Z" });
            //defaultAuthors.Add(new Author() { ID = 3, Nickname = "Kasia Gandor", YtChannelID = "UCUercAwR2To1Zx6GA6ZP2TQ", JoiningDate = "2017-07-02T11:52:09Z" });

            //defaultTracks.Add(new Track() { ID = 1, AuthorID = 3, YtClipID = "kdWNPjXuVOA", ReleaseDate = "2023-03-01T16:18:11Z" });
            //defaultTracks.Add(new Track() { ID = 2, AuthorID = 3, YtClipID = "eivKqL_Ri9o", ReleaseDate = "2022-11-23T16:45:03Z" });
            //defaultTracks.Add(new Track() { ID = 1, AuthorID = 1, YtClipID = "p3yZIwVVxCw", ReleaseDate = "2023-03-26T17:59:49Z" });
            //defaultTracks.Add(new Track() { ID = 1, AuthorID = 2, YtClipID = "0IhmkF50VgE", ReleaseDate = "2023-03-27T19:00:01Z" });
            //defaultTracks.Add(new Track() { ID = 1, AuthorID = 2, YtClipID = "b-WFetQjifc", ReleaseDate = DateTime("2023-02-12T18:01:00Z") });

            context.Authors.AddRange(defaultAuthors);
            context.Tracks.AddRange(defaultTracks);

            //base.Seed(context);
        }
    }
}
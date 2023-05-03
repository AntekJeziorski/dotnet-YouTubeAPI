using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Google.Apis.YouTube.v3.Data;

namespace YouTubeAPI
{   
    /// <summary>
    /// Class which represents database context.
    /// </summary>
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

        /// <summary>
        /// Adds new author to Authors table
        /// </summary>
        /// <param name="author">Author object which will be added to Authors table.</param>
        public void addNewAuthor(Author author)
        {
            using(var context = new YouTubeApiContext())
            {
                context.Authors.Add(author);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds new entry to author history.
        /// </summary>
        /// <param name="authorsHistory">AuthorsHistory object which will be added.</param>
        public void addNewAuthorHistoryEntry(AuthorsHistory authorsHistory)
        {
            using (var context = new YouTubeApiContext())
            {
                context.AuthorsHistory.Add(authorsHistory);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets author history entries by Author id.
        /// </summary>
        /// <param name="Id">Author Id</param>
        public void getAuthorsHistory(string Id)
        {
            using (var context = new YouTubeApiContext())
            {
                var authorsHistoryEntry = context.AuthorsHistory.Where(Channel => Channel.ChannelId == Id).ToList();
                foreach (var author in authorsHistoryEntry)
                {
                    Console.WriteLine(author.ChannelId + ", " + author.ViewCount + ", " + author.SubCount + ", " + author.VideoCount);
                }
            }   
        }

        /// <summary>
        /// Adds new entries for all authors to AuthorsHistory table.
        /// </summary>
        public void updateAllAuthors()
        {
            using (var context = new YouTubeApiContext())
            {
                var authors = context.Authors.Select(Channel => Channel.ChannelId).ToList();
                foreach (var author in authors)
                {
                    var newAuthorEntry = new YouTubeAPI.AuthorsHistory(author);
                    context.addNewAuthorHistoryEntry(newAuthorEntry);
                }
            }
        }


        /// <summary>
        /// Adds new track Tracks table.
        /// </summary>
        /// <param name="track">The track which will be added.</param>
        public void addNewTrack(Track track)
        {
            using (var context = new YouTubeApiContext())
            {
                context.Tracks.Add(track);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds new entry to track history
        /// </summary>
        /// <param name="tracksHistory"></param>
        public void addNewTrackHistoryEntry(TracksHistory tracksHistory)
        {
            using (var context = new YouTubeApiContext())
            {
                context.TracksHistory.Add(tracksHistory);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets track history entries by track Id.
        /// </summary>
        /// <param name="Id">Track's Id</param>
        public void getTracksHistory(string Id)
        {
            using (var context = new YouTubeApiContext())
            {
                var tracksHistoryEntry = context.TracksHistory.Where(Video => Video.VideoId == Id).ToList();
                foreach (var track in tracksHistoryEntry)
                {
                    Console.WriteLine(track.VideoId + ", " + track.ViewCount + ", " + track.LikeCount + ", " + track.CommentCount);
                }
            }
        }

        /// <summary>
        /// Adds new entries for all tracks to TracksHistory table.
        /// </summary>
        public void updateAllTracks()
        {
            using (var context = new YouTubeApiContext())
            {
                var tracks = context.Tracks.Select(Video => Video.VideoId).ToList();
                foreach (var track in tracks)
                {
                    var newTrackEntry = new YouTubeAPI.TracksHistory(track);
                    context.addNewTrackHistoryEntry(newTrackEntry);
                }
            }
        }

    }
    /// <summary>
    /// Database initializer.
    /// Initializes database with seed.
    /// </summary>
    public class YouTubeApiDbInitializer : DropCreateDatabaseAlways<YouTubeApiContext>
    {
        protected override void Seed(YouTubeApiContext context)
        {

            IList<Author> defaultAuthors = new List<Author>();
            IList<Track> defaultTracks = new List<Track>();

            context.Authors.AddRange(defaultAuthors);
            context.Tracks.AddRange(defaultTracks);

        }
    }
}
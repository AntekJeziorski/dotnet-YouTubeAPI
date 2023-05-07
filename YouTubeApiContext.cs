using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Migrations;


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
        public void AddNewAuthor(Author author)
        {
            using(var context = new YouTubeApiContext())
            {
                author.SubscribeTime = DateTime.Now;
                context.Authors.AddOrUpdate(author);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes author from database
        /// </summary>
        /// <param name="Id">Author's Id which will be deleted.</param>
        public void DeleteAuthor(string Id)
        {
            using (var context = new YouTubeApiContext())
            {
                context.AuthorsHistory.RemoveRange(context.AuthorsHistory.Where(Channel => Channel.ChannelId == Id));
                var deleteAuthro = new Author(Id);
                context.Authors.Attach(deleteAuthro);
                context.Authors.Remove(deleteAuthro);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds new entry to author history.
        /// </summary>
        /// <param name="authorsHistory">AuthorsHistory object which will be added.</param>
        public void AddNewAuthorHistoryEntry(AuthorsHistory authorsHistory)
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
        public IList<AuthorsHistory> GetAuthorHistory(string Id)
        {
            using (var context = new YouTubeApiContext())
            {   
                var authorsHistoryEntry = context.AuthorsHistory.Where(Author => Author.ChannelId == Id).ToList();
                return authorsHistoryEntry;
            }   
        }

        /// <summary>
        /// Adds new entries for all authors to AuthorsHistory table.
        /// </summary>
        public void UpdateAllAuthors()
        {
            using (var context = new YouTubeApiContext())
            {
                var authors = context.Authors.Select(Channel => Channel.ChannelId).ToList();
                foreach (var author in authors)
                {
                    var newAuthorEntry = new YouTubeAPI.AuthorsHistory(author);
                    context.AddNewAuthorHistoryEntry(newAuthorEntry);
                }
            }
        }


        public IList<YouTubeAPI.AuthorInfo> GetAuthorInfo()
        {
            using (var context = new YouTubeApiContext())
            {
                var newestEntry = context.AuthorsHistory
                    .GroupBy(o => o.ChannelId)
                    .Select(g => g.OrderByDescending(o => o.AddTime).FirstOrDefault());


                var authors = from history in newestEntry
                              join author in context.Authors
                              on history.ChannelId equals author.ChannelId
                              select new AuthorInfo { AuthorsHistory = history, Author = author };
                authors.OrderByDescending(a => a.Author.SubscribeTime);
                return authors.OrderByDescending(t => t.Author.SubscribeTime).ToList();
            }
        }

        public IList<YouTubeAPI.AuthorInfo> GetMostViewedAuthor()
        {
            using (var context = new YouTubeApiContext())
            {
                var newestEntry = context.AuthorsHistory
                    .GroupBy(o => o.ChannelId)
                    .Select(g => g.OrderByDescending(o => o.AddTime).FirstOrDefault());


                var authors = from history in newestEntry
                              join author in context.Authors
                              on history.ChannelId equals author.ChannelId
                              select new AuthorInfo { AuthorsHistory = history, Author = author };
                authors.OrderByDescending(a => a.Author.SubscribeTime);
                return authors.OrderByDescending(t => t.AuthorsHistory.ViewCount).ToList();
            }
        }

        
        /// <summary>
        /// Gets list of all AuthorInfo objects.
        /// </summary>
        /// <returns>List of AuthorInfo objects.</returns>
        public IList<YouTubeAPI.AuthorInfo> GetMostSubAuthor()
        {
            using (var context = new YouTubeApiContext())
            {
                var newestEntry = context.AuthorsHistory
                    .GroupBy(o => o.ChannelId)
                    .Select(g => g.OrderByDescending(o => o.AddTime).FirstOrDefault());


                var authors = from history in newestEntry
                              join author in context.Authors
                              on history.ChannelId equals author.ChannelId
                              select new AuthorInfo { AuthorsHistory = history, Author = author };
                authors.OrderByDescending(a => a.Author.SubscribeTime);
                return authors.OrderByDescending(t => t.AuthorsHistory.SubCount).ToList();
            }
        }

        /*
         * Tracks handling
         */
        /// <summary>
        /// Adds new track Tracks table.
        /// </summary>
        /// <param name="track">The track which will be added.</param>
        public void AddNewTrack(Track track)
        {
            using (var context = new YouTubeApiContext())
            {
                track.SubscribeTime = DateTime.Now;
                context.Tracks.AddOrUpdate(track);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes track from database.
        /// </summary>
        /// <param name="Id">Track's Id which will be deleted</param>
        public void DeleteTrack(string Id)

        {
            using (var context = new YouTubeApiContext())
            {
                context.TracksHistory.RemoveRange(context.TracksHistory.Where(Channel => Channel.VideoId == Id));
                var deleteTrack = new Track(Id);
                context.Tracks.Attach(deleteTrack);
                context.Tracks.Remove(deleteTrack);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds new entry to track history
        /// </summary>
        /// <param name="tracksHistory"></param>
        public void AddNewTrackHistoryEntry(TracksHistory tracksHistory)
        {
            using (var context = new YouTubeApiContext())
            {
                context.TracksHistory.Add(tracksHistory);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets track track entries by track Id.
        /// </summary>
        /// <param name="Id">Track's Id</param>

        public IList<TracksHistory> GetTrackHistory(string Id)
        {
            using (var context = new YouTubeApiContext())
            {
                var tracksHistoryEntry = context.TracksHistory.Where(Video => Video.VideoId == Id).ToList();
                return tracksHistoryEntry;
            }
        }

        /// <summary>
        /// Adds new entries for all tracks to TracksHistory table.
        /// </summary>
        public void UpdateAllTracks()
        {
            using (var context = new YouTubeApiContext())
            {
                var tracks = context.Tracks.Select(Video => Video.VideoId).ToList();
                foreach (var track in tracks)
                {
                    var newTrackEntry = new YouTubeAPI.TracksHistory(track);
                    context.AddNewTrackHistoryEntry(newTrackEntry);
                }
            }
        }

        /// <summary>
        /// Gets list of all tracks from database.
        /// </summary>
        /// <returns>List of all tracks.</returns>
        public IList<Track> GetAllTracks()

        {
            using (var context = new YouTubeApiContext())
            {
                var tracks = context.Tracks.ToList();
                return tracks;
            }
        }

        /// <summary>
        /// Gets list of all TrackInfo objects.
        /// </summary>
        /// <returns>List of TrackInfo objects.</returns>
        public IList<YouTubeAPI.TrackInfo> GetTrackInfo()
        {
            using (var context = new YouTubeApiContext())
            {
                var newestEntry = context.TracksHistory
                    .GroupBy(o => o.VideoId)
                    .Select(g => g.OrderByDescending(o => o.AddTime).FirstOrDefault());


                var tracks = from history in newestEntry
                              join track in context.Tracks
                              on history.VideoId equals track.VideoId
                              select new TrackInfo { TracksHistory = history, Track = track };

                return tracks.OrderByDescending(t => t.Track.SubscribeTime).ToList();
            }
        }

        public IList<YouTubeAPI.TrackInfo> GetMostViewedTrack()
        {
            using (var context = new YouTubeApiContext())
            {
                var newestEntry = context.TracksHistory
                    .GroupBy(o => o.VideoId)
                    .Select(g => g.OrderByDescending(o => o.AddTime).FirstOrDefault());


                var tracks = from history in newestEntry
                             join track in context.Tracks
                             on history.VideoId equals track.VideoId
                             select new TrackInfo { TracksHistory = history, Track = track };

                return tracks.OrderByDescending(t => t.TracksHistory.ViewCount).ToList();
            }
        }

        public IList<YouTubeAPI.TrackInfo> GetMostLikedTrack()
        {
            using (var context = new YouTubeApiContext())
            {
                var newestEntry = context.TracksHistory
                    .GroupBy(o => o.VideoId)
                    .Select(g => g.OrderByDescending(o => o.AddTime).FirstOrDefault());


                var tracks = from history in newestEntry
                             join track in context.Tracks
                             on history.VideoId equals track.VideoId
                             select new TrackInfo { TracksHistory = history, Track = track };

                return tracks.OrderByDescending(t => t.TracksHistory.LikeCount).ToList();
            }
        }
    }

    /// <summary>
    /// Represents database initializer.
    /// </summary>
    public class YouTubeApiDbInitializer : CreateDatabaseIfNotExists<YouTubeApiContext>
    {
        protected override void Seed(YouTubeApiContext context)
        {
            IList<Author> defaultAuthors = new List<Author>();
            IList<Track> defaultTracks = new List<Track>();

            context.Authors.AddRange(defaultAuthors);
            context.Tracks.AddRange(defaultTracks);
            var newTrack = new YouTubeAPI.Track("2ixECdC615g");
            context.AddNewTrack(newTrack);
        }
    }
}
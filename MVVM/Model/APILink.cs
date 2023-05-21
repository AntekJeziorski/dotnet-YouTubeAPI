using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System.Windows.Forms.DataVisualization.Charting;

namespace YouTubeAPI
{
    /// <summary>
    /// Represents link to YouTubeAPI services.
    /// </summary>
    public class APILink
    {
        /// <summary>
        /// Gets or sets youtube service.
        /// </summary>
        public YouTubeService youtubeService { get; set; }

        /// <summary>
        /// Non-parametric construcor for APILink class. Specifies API key and application name.
        /// </summary>
        public APILink()
        {
            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCjxMdpXjJHt9wKrsQ3yQJq7xUondKbjK4",
                ApplicationName = this.GetType().ToString()
            });
        }

        /// <summary>
        /// Gets Track data identified by VideoId using YoutubeAPI services.
        /// </summary>
        /// <param name="VideoId">ID of searched video.</param>
        /// <returns>Tuple of video's data.</returns>
        public Tuple<string, string, string, string, DateTime, string> GetViedoData(string VideoId)
        {
            string Title = string.Empty;
            string ChannelTitle = string.Empty;
            string ChannelId = string.Empty;
            string Description = string.Empty;
            DateTime ReleaseDate = DateTime.MinValue;
            string ThumbnailMedium = string.Empty;

            // Prepare the request
            VideosResource.ListRequest listRequest = youtubeService.Videos.List("snippet");
            listRequest.Id = VideoId;
            try
            {
                // Execute the request
                VideoListResponse response = listRequest.Execute();
                if (response.PageInfo.TotalResults > 0)
                {
                    // Access the video information
                    foreach (var item in response.Items)
                    {
                        Title = item.Snippet.Title;
                        ChannelTitle = item.Snippet.ChannelTitle;
                        ChannelId = item.Snippet.ChannelId;
                        Description = item.Snippet.Description;
                        ReleaseDate = item.Snippet.PublishedAt ?? DateTime.Now;
                        ThumbnailMedium = item.Snippet.Thumbnails.Medium.Url;
                    }
                }
                else
                {
                    throw new Exception("Wrong Video Id");
                }
            }
            catch (Exception ex)
            {
                // Re-throw exception
                throw ex;
            }
            Console.ReadLine();
            return Tuple.Create(Title, ChannelTitle, ChannelId, Description, ReleaseDate, ThumbnailMedium);
        }

        /// <summary>
        /// Gets statistics about Channel identified by ChannelId using YoutubeAPI services.
        /// </summary>
        /// <param name="ChannelId">ID of channel for which statistics are searched.</param>
        /// <returns>Tuple of author's statistics.</returns>
        public Tuple<long, long, long> GetChannelStats(string ChannelId)
        {
            Int64 ViewCount = Int64.MinValue;
            Int64 SubCount = Int64.MinValue;
            Int64 VideoCount = Int64.MinValue;
            // Prepare the request
            ChannelsResource.ListRequest listRequest = youtubeService.Channels.List("statistics");
            listRequest.Id = ChannelId;
            try
            {
                // Execute the request
                ChannelListResponse response = listRequest.Execute();
                if (response.PageInfo.TotalResults > 0)
                {
                    // Access the channel information
                    foreach (var item in response.Items)
                    {
                        ViewCount = (Int64)item.Statistics.ViewCount;
                        SubCount = (Int64)item.Statistics.SubscriberCount;
                        VideoCount = (Int64)item.Statistics.VideoCount;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error
                throw ex;
            }
            Console.ReadLine();
            return Tuple.Create(ViewCount, SubCount, VideoCount);
        }

        /// <summary>
        /// Gets Author data identified by ChannelId using YoutubeAPI services.
        /// </summary>
        /// <param name="ChannelId">ID of searched artist.</param>
        /// <returns>Tuple of artist's data</returns>
        public Tuple<string, string, DateTime, string, bool> GetChannelData(string ChannelId)
        {
            string Channel = string.Empty;
            string ChannelDescription = string.Empty;
            DateTime JoiningDate = DateTime.MinValue;
            string ThumbnailMedium = string.Empty;
            bool ifId = true;
            // Prepare the request
            ChannelsResource.ListRequest listRequest = youtubeService.Channels.List("snippet");
            listRequest.Id = ChannelId;
            try
            {
                // Execute the request
                ChannelListResponse response = listRequest.Execute();
                if (response.PageInfo.TotalResults > 0)
                {
                    // Access the channel information
                    foreach (var item in response.Items)
                    {
                        Channel = item.Snippet.Title;
                        ChannelDescription = item.Snippet.Description;
                        JoiningDate = item.Snippet.PublishedAt ?? DateTime.Now;
                        ThumbnailMedium = item.Snippet.Thumbnails.Medium.Url;
                    }

                }
                else
                {
                    listRequest.Id = null;
                    listRequest.ForUsername = ChannelId;

                    response = listRequest.Execute();
                    if (response.PageInfo.TotalResults == 1)
                    {
                        // Access the channel information
                        foreach (var item in response.Items)
                        {
                            Channel = item.Id;
                            ChannelDescription = item.Snippet.Description;
                            JoiningDate = item.Snippet.PublishedAt ?? DateTime.Now;
                            ThumbnailMedium = item.Snippet.Thumbnails.Medium.Url;
                            ifId = false;
                        }
                    }
                    else
                    {
                        throw new Exception("Wrong Channel Id or name");
                    }
                }
            }
            catch (Exception ex)
            {
                // Re-throw exception
                throw ex;
            }
            Console.ReadLine();
            return Tuple.Create(Channel, ChannelDescription, JoiningDate, ThumbnailMedium, ifId);

        }

        /// <summary>
        /// Gets statistics about Track identified by VideoId using YoutubeAPI services.
        /// </summary>
        /// <param name="VideoId">ID of video for which statistics are searched.</param>
        /// <returns>Tuple of video's statistics.</returns>
        public Tuple<long, long, long> GetViedoStats(string VideoId)
        {
            Int64 ViewCount = Int64.MinValue;
            Int64 LikeCount = Int64.MinValue;
            Int64 CommentCount = Int64.MinValue;

            // Prepare the request
            VideosResource.ListRequest listRequest = youtubeService.Videos.List("statistics");
            listRequest.Id = VideoId;
            try
            {
                // Execute the request
                VideoListResponse response = listRequest.Execute();
                // Access the video information
                foreach (var item in response.Items)
                {
                    ViewCount = (Int64)item.Statistics.ViewCount;
                    LikeCount = (Int64)item.Statistics.LikeCount;
                    CommentCount = (Int64)item.Statistics.CommentCount;
                }
            }
            catch (Exception ex)
            {
                // Log the error
                throw ex;
            }
            return Tuple.Create(ViewCount, LikeCount, CommentCount);
        }

        
    }
}

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
    /// 
    /// </summary>
    public class APILink
    {
        /// <summary>
        /// 
        /// </summary>
        public YouTubeService youtubeService { get; set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public (string, string, string, string, DateTime, string) GetViedoData(string VideoId)
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
            return (Title, ChannelTitle, ChannelId, Description, ReleaseDate, ThumbnailMedium);
        }
    }
}

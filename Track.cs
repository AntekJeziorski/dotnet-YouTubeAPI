using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouTubeAPI
{
    public class Track
    {
        [Key]
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string ChannelTitle { get; set; }
        public string ChannelId { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ThumbnailMedium { get; set; }
        public DateTime SubscribeTime { get; set; }

        public virtual ICollection<TracksHistory> TracksHistory { get; set; }

        public Track() { }
        public Track(string Id)
        {
            try
            {
                VideoId = Id;
                GetViedoData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
            
        }

        public void GetViedoData()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCTlONe6H40ircsdbuIq87DGV5gZeVv2wc",
                ApplicationName = this.GetType().ToString()
            });
            //string videoId = "nLIp4wd0oXs";
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

                    Console.WriteLine("Title: " + Title);
                    Console.WriteLine("Viedo Id: " + VideoId);
                    Console.WriteLine("Channel Name: " + ChannelTitle);
                    Console.WriteLine("Channel Id: " + ChannelId);
                    }
                }
                else
                {
                    Console.WriteLine("Viedo not found!!!");
                    throw new Exception("Wrong Video Id");
                }
            }
            catch (Exception ex)
            {
                // Re-throw exception
                throw ex;
            }
            Console.ReadLine();
        }
    }
}

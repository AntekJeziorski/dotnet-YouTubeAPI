using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace dotnet_YouTubeAPI
{
    public class YouTubeApiHand
    {
        public string videoId { get; set; }
        public int viewCount { get; set; }
        public int likeCount { get; set; }
        public string title { get; set; }
        public string channelTitle { get; set; }

        public YouTubeApiHand()
        {
            videoId = string.Empty;
            viewCount = 0;
            likeCount = 0;
            title = string.Empty;
            channelTitle = string.Empty;
        }
        public async Task GetViedoData()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCTlONe6H40ircsdbuIq87DGV5gZeVv2wc",
                ApplicationName = this.GetType().ToString()
            });
            string videoId = "nLIp4wd0oXs";
            // Prepare the request
            VideosResource.ListRequest listRequest = youtubeService.Videos.List("snippet,statistics");
            listRequest.Id = videoId;
            try
            {
                // Execute the request
                VideoListResponse response = listRequest.Execute();
                // Access the video information
                foreach (var item in response.Items)
                {
                    viewCount = (int)item.Statistics.ViewCount;
                    likeCount = (int)item.Statistics.LikeCount;
                    title = item.Snippet.Title;
                    channelTitle = item.Snippet.ChannelTitle;

                    Console.WriteLine("Title: " + title);
                    Console.WriteLine("Channel Name: " + channelTitle);
                    Console.WriteLine("View Count: " + viewCount);
                    Console.WriteLine("Like Count: " + likeCount);

                    /*Console.WriteLine("Title: " + item.Snippet.Title);
                    Console.WriteLine("View Count: " + item.Statistics.ViewCount);*/
                }
            }
            catch (Exception e)
            {
                // Log the error
                Console.WriteLine("An error occurred: " + e.Message);
            }
            Console.ReadLine();
        }
    }
}

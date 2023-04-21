﻿using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAPI
{
    public class Author
    {
        [Key]
        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; }
        //public string YtChannelID { get; set; }
        public string ChannelDescription { get; set; }
        public DateTime JoiningDate { get; set; }
        public string ThumbnailMedium { get; set; }


        public Author()
        {
        }
        public Author(string Id)
        {
            ChannelId = Id;
            GetAuthorData();
        }

        public async Task GetAuthorData()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCTlONe6H40ircsdbuIq87DGV5gZeVv2wc",
                ApplicationName = this.GetType().ToString()
            });
            //string videoId = "nLIp4wd0oXs";
            // Prepare the request
            ChannelsResource.ListRequest listRequest = youtubeService.Channels.List("snippet,statistics");
            listRequest.Id = ChannelId;
            try
            {
                // Execute the request
                ChannelListResponse response = listRequest.Execute();
                // Access the video information
                foreach (var item in response.Items)
                {
                    ChannelTitle = item.Snippet.Title;
                    ChannelDescription = item.Snippet.Description;
                    JoiningDate = item.Snippet.PublishedAt ?? DateTime.Now;
                    ThumbnailMedium = item.Snippet.Thumbnails.Medium.Url;

                    Console.WriteLine("Channel Name: " + ChannelTitle);
                    Console.WriteLine("Channel Id: " + ChannelId);
                    Console.WriteLine("Channel Description: " + ChannelDescription);
                }
            }
            catch (Exception e)
            {
                // Log the error
                Console.WriteLine("An error occurred: " + e.Message);
            }
            Console.ReadLine();
        }

        public virtual ICollection<AuthorsHistory> AuthorsHistory { get; set; }
        //public virtual ICollection<Track> Track { get; set; }
    }
}

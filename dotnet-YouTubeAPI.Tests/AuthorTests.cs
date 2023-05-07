using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using YouTubeAPI;

namespace dotnet_YouTubeAPI.Tests
{
    [TestFixture]
    public class AuthorTests
    {
        [Test]
        public void CreateAuthor_ById()
        {
            // Arrange
            string channelId = "valid_mock_id";
            var author = new AuthorMock(channelId);

            // Assert
            Assert.AreEqual(author.ChannelId, channelId);
            Assert.AreEqual(author.ChannelTitle, "Mock Channel Title");
            Assert.AreEqual(author.ChannelDescription, "Mock Channel Description");
            Assert.AreEqual(author.JoiningDate, new DateTime(2023, 07, 05, 12, 00, 00));
            Assert.AreEqual(author.ThumbnailMedium, "Mock Thumbnail Url");
            Assert.AreEqual(author.SubscribeTime, new DateTime(2023, 07, 05, 21, 37, 00));
        }

        [Test]
        public void GetAuthorData_ValidId_ReturnsData()
        {
            // Arrange
            string channelId = "valid_mock_id";
            var author = new AuthorMock();
            author.ChannelId = channelId;

            // Act
            author.GetChannelData();

            // Assert
            Assert.AreEqual(author.ChannelId, channelId);
            Assert.AreEqual(author.ChannelTitle, "Mock Channel Title");
            Assert.AreEqual(author.ChannelDescription, "Mock Channel Description");
            Assert.AreEqual(author.JoiningDate, new DateTime(2023, 07, 05, 12, 00, 00));
            Assert.AreEqual(author.ThumbnailMedium, "Mock Thumbnail Url");
            Assert.AreEqual(author.SubscribeTime, new DateTime(2023, 07, 05, 21, 37, 00));

        }

        [Test]
        public void GetAuthorData_InvalidId_ThrowsException()
        {
            // Arrange
            string channelId = "invalid_id";

            // Act & Assert
            Assert.Throws<Exception>(() => new YouTubeAPI.Author("invalid_id"));
        }

        [Test]
        public void CreateAuthorsHistory_ById()
        {
            string channelId = "valid_mock_id";
            var authorsHistory = new AuthorsHistoryMock(channelId);

            // Assert
            Assert.AreEqual(authorsHistory.ChannelId, channelId);
            Assert.AreEqual(authorsHistory.ViewCount, 1234);
            Assert.AreEqual(authorsHistory.SubCount, 1234);
            Assert.AreEqual(authorsHistory.VideoCount, 1234);
            Assert.AreEqual(authorsHistory.AddTime, new DateTime(2023, 07, 05, 21, 37, 00));
        }

        [Test]
        public void CreateAuthorsHistory_InvalidId()
        {
            // Arrange & Act
            string channelId = "invalid_id";
            var authorsHistory = new AuthorsHistory();
            authorsHistory.ChannelId = channelId;

            // Assert
            Assert.AreEqual(authorsHistory.ChannelId, channelId);
            Assert.AreEqual(authorsHistory.ViewCount, 0);
            Assert.AreEqual(authorsHistory.SubCount, 0);
            Assert.AreEqual(authorsHistory.VideoCount, 0);
            Assert.AreEqual(authorsHistory.AddTime, new DateTime(0001, 01, 01, 00, 00, 00));
        }

        [Test]
        public void GetAuthorStats_ValidId_ReturnsData()
        {
            // Arrange
            string channelId = "invalid_id";
            var authorsHistory = new AuthorsHistoryMock();
            authorsHistory.ChannelId = channelId;

            // Act
            authorsHistory.GetChannelStats();

            // Assert
            Assert.AreEqual(authorsHistory.ChannelId, channelId);
            Assert.AreEqual(authorsHistory.ViewCount, 1234);
            Assert.AreEqual(authorsHistory.SubCount, 1234);
            Assert.AreEqual(authorsHistory.VideoCount, 1234);
            Assert.AreEqual(authorsHistory.AddTime, new DateTime(0001, 01, 01, 00, 00, 00));
        }
    }
}

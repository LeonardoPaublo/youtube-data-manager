using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeDataManager.Models;
using YouTubeDataManager.Repositories;
using YouTubeDataManager.Services;

namespace YouTubeDataManager.Tests.Services
{
    [TestClass]
    public class VideosServiceTests
    {
        private Mock<IVideosRepository> _videosRepositoryMock;
        private Mock<IYouTubeApiService> _youTubeApiServiceMock;
        private VideosService _videosService;

        [TestInitialize]
        public void Setup()
        {
            _videosRepositoryMock = new Mock<IVideosRepository>();
            _youTubeApiServiceMock = new Mock<IYouTubeApiService>();
            _videosService = new VideosService(_videosRepositoryMock.Object, _youTubeApiServiceMock.Object);
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnFilteredVideos()
        {
            var videos = new List<Video> { new Video { Id = "1", Title = "Test Video" } };
            _videosRepositoryMock.Setup(repo => repo.GetFilteredVideos(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<string>()))
                .ReturnsAsync(videos);

            var result = await _videosService.GetAll("Test", "Author", DateTime.Now, "Query");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task Insert_ShouldReturnInsertedVideo()
        {
            var video = new Video { Id = "1", Title = "New Video" };
            _videosRepositoryMock.Setup(repo => repo.Insert(It.IsAny<Video>())).ReturnsAsync(video);

            var result = await _videosService.Insert(video);

            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Id);
        }

        [TestMethod]
        public async Task Update_WhenVideoExists_ShouldReturnUpdatedVideo()
        {
            var video = new Video { Id = "1", Title = "Updated Video" };
            _videosRepositoryMock.Setup(repo => repo.GetById("1")).ReturnsAsync(video);
            _videosRepositoryMock.Setup(repo => repo.Update(It.IsAny<Video>())).ReturnsAsync(video);

            var result = await _videosService.Update("1", video);

            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Video", result.Title);
        }

        [TestMethod]
        public async Task Update_WhenVideoDoesNotExist_ShouldReturnNull()
        {
            _videosRepositoryMock.Setup(repo => repo.GetById("1")).ReturnsAsync((Video)null);

            var result = await _videosService.Update("1", new Video());

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnTrue_WhenDeletionSuccessful()
        {
            _videosRepositoryMock.Setup(repo => repo.SoftDelete("1")).ReturnsAsync(true);

            var result = await _videosService.Delete("1");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task FetchAndStoreYouTubeVideos_ShouldReturnInsertedCount()
        {
            var youtubeVideos = new List<Video> { new Video { Id = "1", Title = "YouTube Video" } };
            _youTubeApiServiceMock.Setup(api => api.GetVideosAsync()).ReturnsAsync(youtubeVideos);
            _videosRepositoryMock.Setup(repo => repo.InsertBatch(It.IsAny<List<Video>>())).Returns(Task.CompletedTask);

            var result = await _videosService.FetchAndStoreYouTubeVideos();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task FetchAndStoreYouTubeVideos_WhenNoVideos_ShouldReturnZero()
        {
            _youTubeApiServiceMock.Setup(api => api.GetVideosAsync()).ReturnsAsync(new List<Video>());

            var result = await _videosService.FetchAndStoreYouTubeVideos();

            Assert.AreEqual(0, result);
        }
    }
}